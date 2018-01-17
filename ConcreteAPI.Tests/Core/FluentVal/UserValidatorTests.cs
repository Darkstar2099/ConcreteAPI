﻿using System;
using System.Linq;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Services;
using ConcreteAPI.Core.FluentVal;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcreteAPI.Tests.Core.FluentVal
{
    [TestClass]
    public class UserValidatorTests
    {

        // O PhoneValidator é testado por tabela em alguns dos testes a seguir.

        private const string NAME = "Tony Stark";
        private const string EMAIL = "tony.stark@starksenterprise.com";
        private const string PASSWORD = "Iron$M";

        private const string DDD1 = "021";
        private const string NUMBER1 = "55555656";

        private const string DDD2 = "022";
        private const string NUMBER2 = "87877878";

        private readonly UserValidator _userValidator;

        public UserValidatorTests()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            _userValidator = new UserValidator();
        }

        [TestMethod]
        public void ValidaUserCom2Phones_RetornaUserE2PhonesCriados()
        {
            var user = new User(NAME, EMAIL, PASSWORD)
            {
                CreationDate = DateTime.Now
            };

            var phone1 = new Phone(user.Id, DDD1, NUMBER1);
            var phone2 = new Phone(user.Id, DDD2, NUMBER2);

            user.Phones.Add(phone1);
            user.Phones.Add(phone2);

            var validatorReturn = _userValidator.Validate(user);

            Assert.IsTrue(validatorReturn.IsValid);
            Assert.AreEqual(NAME, user.Name);
            Assert.AreEqual(EMAIL, user.Email);
            Assert.IsTrue(new Encryption().ComparePasswords(PASSWORD, user.Password));

            //Verifica se os 2 phones foram criados
            Assert.IsNotNull(user.Phones);
            Assert.AreEqual(2, user.Phones.Count);
            
            //Verifica o primeiro phone
            Assert.AreEqual(DDD1, user.Phones.First().Ddd);
            Assert.AreEqual(NUMBER1, user.Phones.First().Number);

            //Verifica 0 segundo phone
            Assert.AreEqual(DDD2, user.Phones.Last().Ddd);
            Assert.AreEqual(NUMBER2, user.Phones.Last().Number);
        }

        [TestMethod]
        public void ValidaUserComPhoneInvalido_RetornaErrosNaValidacao()
        {
            var user = new User(NAME, EMAIL, PASSWORD)
            {
                CreationDate = DateTime.Now
            };

            //Usando Phone vazios para obter erro na validação
            user.Phones.Add(new Phone(user.Id, "", ""));

            var validationResult = _userValidator.Validate(user);

            //Verifica se a Validação retornou com problemas
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Any());

            //Verifica se 2 Erros foram retornados
            Assert.AreEqual(2, validationResult.Errors.Count);
        }
    }
}
