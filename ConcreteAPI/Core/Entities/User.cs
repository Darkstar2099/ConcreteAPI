using System;
using System.Collections.Generic;
using ConcreteAPI.Core.Services;

namespace ConcreteAPI.Core.Entities
{
    public class User
    {
        protected User()
        {
            Id = Guid.NewGuid();
            Phones = new List<Phone>();
        }

        public User(string name, string email, string password)
            : this()
        {
            Name = name;
            Email = email;
            ChangePassword(password);
        }

        public User(string name, string email, string password, DateTime creationDate)
            : this()
        {
            Name = name;
            Email = email;
            CreationDate = creationDate;
            ChangePassword(password);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }

        //Método para a troca de password.
        public void ChangePassword(string newPassword)
        {
            Password = new Encryption().EncryptPassword(newPassword);
        }

        //Método de validação de password.
        public bool PasswordValidation(string password)
        {
            return new Encryption().ComparePasswords(password, Password);
        }
    }
}