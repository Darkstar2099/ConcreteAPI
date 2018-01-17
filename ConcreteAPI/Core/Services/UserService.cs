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
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly UserValidator _validator;
        private readonly IToken _token;

        public UserService(IUserRepository repository, UserValidator validator, IToken token)
        {
            _repository = repository;
            _validator = validator;
            _token = token;
        }

        public Task<bool> EmailRegisteredAsync(string email)
        {
            return _repository.EmailRegisteredAsync(email);
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _repository.GetByEmailAsync(email);
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