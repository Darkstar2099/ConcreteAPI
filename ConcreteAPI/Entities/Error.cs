using Newtonsoft.Json;

namespace ConcreteAPI.Entities    
{
    public class Error
    {
        public Error()
        {            
        }

        public Error(int statusCode, string mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }

        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty(PropertyName = "mensagem")]
        public string Mensagem { get; set; }

    }
}