﻿using System;
using System.Linq;
using ConcreteAPI.Core.Commands.Send.Users;
using ConcreteAPI.Core.Commands.Return.Phones;
using ConcreteAPI.Core.Commands.Return.Users;
using ConcreteAPI.Core.Entities;

namespace ConcreteAPI.Core.Mapper
{
    public class UserMapper
    {
        public User MapFrom(NewUserCommand command)
        {
            var user = new User(command.Name, command.Email, command.Password, DateTime.Now);
            command.Phones.ForEach(phone => user.Phones.Add(new Phone(user.Id, phone.Ddd, phone.Number)));
            return user;
        }

        public UserReturnCommand OutputFrom(User user)
        {
            return new UserReturnCommand
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreationDate = user.CreationDate,
                UpdateDate = user.UpdateDate,
                LastLoginDate = user.LastLoginDate,
                Token = user.Token,
                Phones = user.Phones.Select(phone => new PhoneReturnCommand(phone.Ddd, phone.Number)).ToList()
            };
        }

    }
}