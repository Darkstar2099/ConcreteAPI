﻿using System.Collections.Generic;
using ConcreteAPI.Core.Commands.Send.Phones;
using ConcreteAPI.Core.Commands.Interfaces;
using Newtonsoft.Json;

namespace ConcreteAPI.Core.Commands.Send.Users
{
    public class NewUserCommand : ICommand
    {
        public NewUserCommand()
        {
            Phones = new List<NewPhoneCommand>();
        }

        public NewUserCommand(string name, string email, string password)
            : this()
        {
            Name = name;
            Email = email;
            Password = password;
        }

        [JsonProperty(PropertyName = "nome")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "senha")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "telefones")]
        public List<NewPhoneCommand> Phones { get; set; }

    }
}