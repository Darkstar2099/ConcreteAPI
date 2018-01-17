using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Services;

namespace ConcreteAPI.Tests.Core.Entities
{
    [TestClass]
    public class UserTests
    {
        private const string NAME = "Peter Parker";
        private const string EMAIL = "email@marvel.com";
        private const string PASSWORD = "121212";

        [TestMethod]
        public void CriaUser_UserCriado()
        {
            // Arrange
            var user = new User(NAME, EMAIL, PASSWORD);

            // Act
            var encryptedPassword = new Encryption().EncryptPassword(PASSWORD);

            // Assert
            // Verifica se o usuário foi criado corretamente
            Assert.IsTrue(Guid.Empty != user.Id);
            Assert.AreEqual(NAME, user.Name);
            Assert.AreEqual(EMAIL, user.Email);
            Assert.AreEqual(encryptedPassword, user.Password);

            // Verifica se os Telefones foram criados
            Assert.IsFalse(user.Phones == null);
            Assert.IsFalse(user.Phones.Any());
       }
    }
}
