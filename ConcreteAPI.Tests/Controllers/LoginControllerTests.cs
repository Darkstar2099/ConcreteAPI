﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConcreteAPI.Core.Commands.Send.Users;
using ConcreteAPI.Core.Commands.Return.Users;
using ConcreteAPI.Core.Constants;
using ConcreteAPI.Controllers;
using ConcreteAPI.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcreteAPI.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTests : AbstractControllerTest
    {
        private readonly string _actionName = nameof(LoginController).Replace("Controller", "");
        private readonly string _signUpActionName = nameof(SignUpController).Replace("Controller", "");

        [TestMethod]
        public async void LogaERetornaUsuarioCadastrado()
        {
            var name = Guid.NewGuid().ToString("N");
            var email = $"{Guid.NewGuid():N}@test.io";
            var password = Guid.NewGuid().ToString();
            
            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_signUpActionName, new NewUserCommand(name, email, password));
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                
                var user = await response.Content.ReadAsAsync<UserReturnCommand>();
                Assert.IsNotNull(user);
            }
            
            using (var cliente = CreateClient())
            {
                var response = await cliente.PostAsJsonAsync(_actionName, new LogInUserCommand(email, password));
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                
                var user = await response.Content.ReadAsAsync<UserReturnCommand>();
                Assert.IsNotNull(user);
                Assert.AreEqual(name, user.Name);
                Assert.AreEqual(email, user.Email);
            }
        }
    }
}
