using ConcreteAPI.Core.Commands.Interfaces;
using Newtonsoft.Json;

namespace ConcreteAPI.Core.Commands.Send.Users
{
    public class LogInUserCommand : ICommand
    {
        public LogInUserCommand()
        {            
        }

        public LogInUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "senha")]
        public string Password { get; set; }
    }
}