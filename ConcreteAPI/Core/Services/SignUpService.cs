﻿using System;
using System.Threading.Tasks;
using ConcreteAPI.Core.Constants;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Extensions;
using ConcreteAPI.Core.Repositories.Interfaces;
using ConcreteAPI.Core.Services.Interfaces;
using ConcreteAPI.Core.FluentVal;
using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Core.Services
{
    public class SignUpService
    {
        private readonly ISignUpRepository _repository;
        private readonly UserValidator _validator;
        private readonly IToken _token;

        public SignUpService(ISignUpRepository repository, UserValidator validator, IToken token)
        {
            _repository = repository;
            _validator = validator;
            _token = token;
        }

        public async Task<CommandReturn> InserirAsync(User user)
        {
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
                return new CommandReturn(validationResult.Errors);

            if (user.CreationDate<DateTime.Now.AddSeconds(-60))
                return new CommandReturn($"A Data de Criação: {user.CreationDate:G} é inválida");

            if (await _repository.EmailRegisteredAsync(user.Email))
                return new CommandReturn(Messages.EmailAlreadyUsed);

            var result = await _repository.InsertAsync(user);
            if (result.Failed)
                return result;

            return await GravarLoginAsync(user);
        }

        public async Task<CommandReturn> GravarLoginAsync(User user)
        {
            user.LastLoginDate = DateTime.Now;
            user.UpdateDate = user.LastLoginDate;

            user.Token = _token.GenerateToken(user);

            if (user.Token.IsEmpty())
                return new CommandReturn(Messages.TokenNotFound);

            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
                return new CommandReturn(validationResult.Errors);

            return await _repository.UpdateAsync(user);
        }

    }
}