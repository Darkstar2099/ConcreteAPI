﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConcreteAPI.Core.Commands.Send.Users;
using ConcreteAPI.Core.Commands.Return.Users;
using ConcreteAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcreteAPI.Tests.Controllers
{
    [TestClass]
    public class Profile1ControllerTest : AbstractControllerTest
    {

        private readonly string _actionName = nameof(Profile1Controller).Replace("Controller", "");
        private readonly string _signUpActionName = nameof(SignUpController).Replace("Controller", "");

        [TestMethod]
        public async void PegaDadosUsuario_RetornaDadosDoUsuario()
        {

            var name = Guid.NewGuid().ToString();
            var email = $"{DateTime.Now:yyyyMMddhhmmss}@test.io";
            var password = Guid.NewGuid().ToString("N");
            
            UserReturnCommand user;
            
            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_signUpActionName, new NewUserCommand(name, email, password));
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                
                user = await response.Content.ReadAsAsync<UserReturnCommand>();
                Assert.IsNotNull(user);
            }
            
            using (var client = CreateClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                
                var response = await client.GetAsync($"{_actionName}/{user.Id}");
                var profile = await response.Content.ReadAsAsync<UserReturnCommand>();
                Assert.IsNotNull(profile);
                
                Assert.AreEqual(user.Id, profile.Id);
                Assert.AreEqual(user.Name, profile.Name);
                Assert.AreEqual(user.Email, profile.Email);
            }

        }
    }
}
