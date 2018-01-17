﻿using System;
using System.Collections.Generic;
using ConcreteAPI.Core.Commands.Return.Phones;
using Newtonsoft.Json;

namespace ConcreteAPI.Core.Commands.Return.Users
{
    public class UserReturnCommand
    {
        public UserReturnCommand()
        {
            Phones = new List<PhoneReturnCommand>();
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "nome")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "senha")]
        public string Password => "***";

        [JsonProperty(PropertyName = "data_criacao")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "data_atualizacao")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty(PropertyName = "ultimo_login")]
        public DateTime? LastLoginDate { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "telefones")]
        public ICollection<PhoneReturnCommand> Phones { get; set; }

    }
}