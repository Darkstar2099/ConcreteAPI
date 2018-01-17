using ConcreteAPI.Core.Commands.Interfaces;
using Newtonsoft.Json;

namespace ConcreteAPI.Core.Commands.Send.Phones
{
    public class NewPhoneCommand : ICommand
    {
        public NewPhoneCommand()
        {            
        }

        public NewPhoneCommand(string ddd, string number)
        {
            Ddd = ddd;
            Number = number;
        }

        [JsonProperty(PropertyName = "ddd")]
        public string Ddd { get; set; }

        [JsonProperty(PropertyName = "numero")]
        public string Number { get; set; }

    }
}