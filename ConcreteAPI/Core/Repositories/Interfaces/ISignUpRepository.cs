﻿using System.Threading.Tasks;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Core.Repositories.Interfaces
{
    public interface ISignUpRepository
    {
        Task<bool> EmailRegisteredAsync(string email);
        Task<CommandReturn> InsertAsync(User user);
        Task<CommandReturn> UpdateAsync(User user);
    }
}
