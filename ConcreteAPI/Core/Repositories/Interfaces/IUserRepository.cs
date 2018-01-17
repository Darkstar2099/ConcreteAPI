﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailRegisteredAsync(string email);
        Task<User> GetByIdAsync(Guid id, params Expression<Func<User, object>>[] includes);
        Task<User> GetByEmailAsync(string email, params Expression<Func<User, object>>[] includes);        
        Task<CommandReturn> UpdateAsync(User entity);
    }
}
