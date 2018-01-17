﻿using System.Threading.Tasks;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Repositories.Interfaces;
using ConcreteAPI.Core.Common;
using System.Data.SqlClient;


namespace ConcreteAPI.Persistence.ADO.NET.Repositories
{
    public class SignUpRepository : ADOAbstractRepository, ISignUpRepository
    {
        public SignUpRepository(ConcreteAPIDbConnection connection)
            : base(connection)
        {
        }

        public async Task<bool> EmailRegisteredAsync(string email)
        {
            const string sql = "SELECT coalesce(count(*), 0) from \"Users\" WHERE lower(\"Email\") like lower(@Email)";

            var result = await Connection.ExecuteCountAsync(sql, new SqlParameter("Email", email)) > 0;

            return result;
        }

        public async Task<CommandReturn> InsertAsync(User user)
        {

            bool returnedAllPhonesSuccess = true;

            const string sqlUser = " INSERT INTO \"Users\" (\"Id\", \"Name\", \"Email\", \"Password\", \"CreationDate\") " +
                " VALUES (@Id, @Name, @Email, @Password, @CreationDate) ";

            const string sqlPhone = " INSERT INTO \"Phones\" (\"Id\", \"UserId\", \"Ddd\", \"Number\") " +
                " VALUES (@Id, @UserId, @Ddd, @Number) ";

            var returnedUserSuccess = await Connection.ExecuteCommandAsync(sqlUser,
                new SqlParameter("Id", user.Id),
                new SqlParameter("Name", user.Name),
                new SqlParameter("Email", user.Email),
                new SqlParameter("Password", user.Password),
                new SqlParameter("CreationDate", user.CreationDate)) > 0;

            if (returnedUserSuccess) {
                var phones = user.Phones;
                foreach (var phone in phones) {
                    var returnedPhoneSuccess = await Connection.ExecuteCommandAsync(sqlPhone,
                    new SqlParameter("Id", phone.Id),
                    new SqlParameter("UserId", phone.UserId),
                    new SqlParameter("Ddd", phone.Ddd),
                    new SqlParameter("Number", phone.Number)) > 0;
                    if (!returnedPhoneSuccess && returnedAllPhonesSuccess)
                        returnedAllPhonesSuccess = false;
                }
            }

            if (returnedUserSuccess && returnedAllPhonesSuccess)
                return Success();

            return Error("Não foi possível inserir o registro de Usuário");
        }

        public async Task<CommandReturn> UpdateAsync(User user)
        {
            const string sql = " UPDATE \"Users\" set " +
                " \"UpdateDate\" = @UpdateDate, " +
                " \"LastLoginDate\" = @LastLoginDate, " +
                " \"Token\" = @Token " +
                " WHERE \"Id\" = @Id ";

            var returnedSuccess = await Connection.ExecuteCommandAsync(sql,
                new SqlParameter("UpdateDate", user.UpdateDate),
                new SqlParameter("LastLoginDate", user.LastLoginDate),
                new SqlParameter("Token", user.Token),
                new SqlParameter("Id", user.Id)) > 0;

            if (returnedSuccess)
                return Success();

            return Error("Não foi possível alterar o registro de Usário");
        }

    }
}