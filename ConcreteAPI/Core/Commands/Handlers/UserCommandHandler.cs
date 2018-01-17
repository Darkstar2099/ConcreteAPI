﻿using System.Net;
using System.Threading.Tasks;
using ConcreteAPI.Core.Commands.Send.Users;
using ConcreteAPI.Core.Commands.Interfaces;
using ConcreteAPI.Core.Constants;
using ConcreteAPI.Core.Mapper;
using ConcreteAPI.Core.Services;
using ConcreteAPI.Core.FluentVal;
using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Core.Commands.Handlers
{
    public class UserCommandHandler : AbstractCommandHandler, ICommandHandler<GetUserByIdCommand>, ICommandHandler<LogInUserCommand>
    {

        private readonly UserService _service;
        private readonly EmailValidator _emailValidator;

        public UserCommandHandler(UserService service, EmailValidator emailValidator)
        {
            _service = service;
            _emailValidator = emailValidator;
        }

        public async Task<CommandReturn> ExecuteAsync(GetUserByIdCommand command)
        {
            if (command == null)
                return Error(Messages.InvalidCommand);

            var user = await _service.GetByIdAsync(command.Id);
            if (user == null)
                return Error(Messages.UserNotRegistered);

            var output = new UserMapper().OutputFrom(user);
            return Success(output);
        }

        public async Task<CommandReturn> ExecuteAsync(LogInUserCommand command)
        {
            if (command == null)
                return Error(Messages.InvalidCommand);

            var emailValidationResult = _emailValidator.Validate(new Email(command.Email));
            if (!emailValidationResult.IsValid)
                return Error(emailValidationResult);

            var user = await _service.GetByEmailAsync(command.Email);

            if (user == null)
                // Como o email não existe, retornar UserOrPassNotValid
                return Error(Messages.UserOrPassNotValid);

            if (!user.PasswordValidation(command.Password))
                // Como a senha não confere, retornar UserOrPassNotValid e (401) Unauthorized
                return new CommandReturn(Messages.UserOrPassNotValid) { ErrorCode = HttpStatusCode.Unauthorized };

            var result = await _service.GravarLoginAsync(user);
            if (result.Failed)
                return result;

            var output = new UserMapper().OutputFrom(user);
            return Success(output);
        }

    }
}