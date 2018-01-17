﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using ConcreteAPI.Core.Commands.Handlers;
using ConcreteAPI.Core.Commands.Send.Users;
using ConcreteAPI.Filters;

namespace ConcreteAPI.Controllers
{
    [JwtAuthorize]
    public class Profile1Controller : AbstractController
    {
        private readonly UserCommandHandler _commandHandler;

        public Profile1Controller(UserCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // GET api/profile1/{id}, lembrar de usar o header Authorization com valor "Bearer {token}" ao fazer o GET
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid? id)
        {
            var response = await _commandHandler.ExecuteAsync(new GetUserByIdCommand(id ?? Guid.Empty));
            return ProcessResult(response);
        }
    }
}
