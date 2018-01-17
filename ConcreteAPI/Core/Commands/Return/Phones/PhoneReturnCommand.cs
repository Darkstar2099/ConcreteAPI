using ConcreteAPI.Core.Commands.Interfaces;
using Newtonsoft.Json;

namespace ConcreteAPI.Core.Commands.Return.Phones
{
    public class PhoneReturnCommand : ICommand
    {
        public PhoneReturnCommand()
        {            
        }

        public PhoneReturnCommand(string ddd, string number)
        {
            Ddd = ddd;
            Number = number;
        }

        [JsonProperty(PropertyName = "ddd")]
        public string Ddd { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

    }
}