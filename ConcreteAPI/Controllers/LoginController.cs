﻿using System.Threading.Tasks;
using System.Web.Http;
using ConcreteAPI.Core.Commands.Handlers;
using ConcreteAPI.Core.Commands.Send.Users;

namespace ConcreteAPI.Controllers
{
    public class LoginController : AbstractController
    {
        private readonly UserCommandHandler _commandHandler;

        public LoginController(UserCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // POST api/login/
        [HttpPost]
        public async Task<IHttpActionResult> Post(LogInUserCommand command)
        {
            var result = await _commandHandler.ExecuteAsync(command);
            return ProcessResult(result);
        }

    }
}