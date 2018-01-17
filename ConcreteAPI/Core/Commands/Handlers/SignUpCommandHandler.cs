﻿using System.Threading.Tasks;
using ConcreteAPI.Core.Commands.Send.Users;
using ConcreteAPI.Core.Commands.Interfaces;
using ConcreteAPI.Core.Constants;
using ConcreteAPI.Core.Mapper;
using ConcreteAPI.Core.Services;
using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Core.Commands.Handlers
{
    public class SignUpCommandHandler : AbstractCommandHandler, ICommandHandler<NewUserCommand>
    {
        private readonly SignUpService _service;

        public SignUpCommandHandler(SignUpService service)
        {
            _service = service;
        }

        public async Task<CommandReturn> ExecuteAsync(NewUserCommand command)
        {
            if (command == null)
                return Error(Messages.InvalidCommand);

            var user = new UserMapper().MapFrom(command);

            var result = await _service.InserirAsync(user);
            if (result.Failed)
                return result;

            var output = new UserMapper().OutputFrom(user);
            return Success(output);
        }

    }
}