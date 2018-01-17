﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Repositories.Interfaces;
using ConcreteAPI.Core.Common;
using ConcreteAPI.Persistence.EF.Context;

namespace ConcreteAPI.Persistence.EF.Repositories
{
    public class UserRepository : EFAbstractRepository<User>, IUserRepository
    {
        public UserRepository(ConcreteAPIContext context)
            : base(context)
        {
        }

        // Implementação do Método GetByIdAsync (IUserRepository)
        public Task<User> GetByIdAsync(Guid id, params Expression<Func<User, object>>[] includes)
        {
            IQueryable<User> query = Set;

            foreach (var include in includes ?? new Expression<Func<User, object>>[] { })
                query = query.Include(include);

            return query.Include(x => x.Phones).SingleOrDefaultAsync(x => x.Id == id);
        }

        // Implementação do Método GetByEmailAsync (IUserRepository)
        public Task<User> GetByEmailAsync(string email, params Expression<Func<User, object>>[] includes)
        {
            IQueryable<User> query = Set;

            foreach (var include in includes ?? new Expression<Func<User, object>>[] { })
                query = query.Include(include);

            return query.Include(x => x.Phones).SingleOrDefaultAsync(x => x.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));

        }

        // Implementação do Método EmailRegisteredAsync (IUserRepository)
        public Task<bool> EmailRegisteredAsync(string email)
        {
            return Set.AsNoTracking().Where(x => x != null).AnyAsync(x => x.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        // Implementação do Método InsertAsync (IUserRepository)
        //public Task<MethodReturn> InsertAsync(User user)
        //{
        //    return AddAsync(user);
        //}

        // Implementação do Método UpdateAsync (IUserRepository)
        public Task<CommandReturn> UpdateAsync(User user)
        {
            // Usa o ChangeAsync do EFAbstractRepository
            return ChangeAsync(user);
        }

    }
}