﻿using System;
using System.Linq;
using ConcreteAPI.Core.Commands.Send.Phones;
using ConcreteAPI.Core.Commands.Send.Users;
using ConcreteAPI.Core.Extensions;
using ConcreteAPI.Core.Mapper;
using ConcreteAPI.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcreteAPI.Tests.Core.Mapper
{
    [TestClass]
    public class UserMapperTests
    {

        private const string NAME = "Hal Jordan";
        private const string EMAIL = "hal.jordan@ferrisaircraft.com";
        private const string PASSWORD = "GrnL34";

        private const string DDD1 = "021";
        private const string PHONE1 = "5556969";

        private const string DDD2 = "023";
        private const string PHONE2 = "9555-6789";

        [TestMethod]
        public void CriaUserCorretamente_UserCriado()
        {
            // Arrange
            var newUserCommand = new NewUserCommand(NAME, EMAIL, PASSWORD);

            newUserCommand.Phones.Add(new NewPhoneCommand(DDD1, PHONE1));
            newUserCommand.Phones.Add(new NewPhoneCommand($"({DDD2})", PHONE2));
            
            // Act
            var user = new UserMapper().MapFrom(newUserCommand);
            
            // Assert
            Assert.IsNotNull(user);
            Assert.AreNotEqual(Guid.Empty, user.Id);
            Assert.IsNotNull(user.Phones);
            Assert.IsTrue(user.Phones.Any());
            Assert.AreEqual(2, user.Phones.Count);
            
            Assert.AreEqual(NAME, user.Name);
            Assert.AreEqual(EMAIL, user.Email);
            Assert.IsTrue(new Encryption().ComparePasswords(PASSWORD, user.Password));
            
            Assert.AreEqual(user.Phones.First().Ddd, DDD1);
            Assert.AreEqual(user.Phones.First().Number, PHONE1);
            
            Assert.AreEqual(user.Phones.Last().Ddd, DDD2.OnlyNumbers());
            Assert.AreEqual(user.Phones.Last().Number, PHONE2.OnlyNumbers());

        }
    }
}
