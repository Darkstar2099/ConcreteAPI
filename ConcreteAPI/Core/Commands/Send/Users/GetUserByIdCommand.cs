﻿using System;
using ConcreteAPI.Core.Commands.Interfaces;

namespace ConcreteAPI.Core.Commands.Send.Users
{
    public class GetUserByIdCommand : ICommand
    {
        public GetUserByIdCommand()
        {            
        }

        public GetUserByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }
}