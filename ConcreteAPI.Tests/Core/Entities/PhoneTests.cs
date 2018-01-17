using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcreteAPI.Core.Entities;

namespace ConcreteAPI.Tests.Core.Entities
{
    [TestClass]
    public class PhoneTests
    {
        private const string DDD = "021";
        private const string NUMBER = "98989898";

        [TestMethod]
        public void CriaPhone_PhoneCriado()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var phone = new Phone(userId, DDD, NUMBER);

            // Assert
            Assert.IsTrue(Guid.Empty != phone.Id);
            Assert.IsTrue(userId == phone.UserId);
            Assert.AreEqual(DDD, phone.Ddd);
            Assert.AreEqual(NUMBER, phone.Number);
        }

        [TestMethod]
        public void SalvaSomenteNumeros_SomenteNumerosSalvos()
        {
            // Act
            var phone = new Phone(Guid.NewGuid(), $"({DDD})", NUMBER.Insert(4, "-"));

            // Assert
            Assert.AreEqual(DDD, phone.Ddd);
            Assert.AreEqual(NUMBER, phone.Number);
        }
    }
}
