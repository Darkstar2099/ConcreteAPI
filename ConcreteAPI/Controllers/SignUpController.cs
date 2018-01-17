﻿using System.Threading.Tasks;
using System.Web.Http;
using ConcreteAPI.Core.Commands.Handlers;
using ConcreteAPI.Core.Commands.Send.Users;

namespace ConcreteAPI.Controllers
{
    public class SignUpController : AbstractController
    {
        private readonly SignUpCommandHandler _commandHandler;

        public SignUpController(SignUpCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
        
        // POST api/signup/
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] NewUserCommand command)
        {
            var result = await _commandHandler.ExecuteAsync(command);
            return ProcessResult(result);
        }

    }
}