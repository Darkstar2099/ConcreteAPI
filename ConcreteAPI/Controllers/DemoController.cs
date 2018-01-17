using System.Collections.Generic;
using System.Web.Http;

namespace ConcreteAPI.Controllers
{
    public class DemoController : ApiController
    {
        // Controller criado para testes iniciais da Web API
        // Pode ser removido sem problemas

        // GET api/demo 
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }

        // GET api/demo/{id} 
        public string Get(int id)
        {
            return "Hello, World!";
        }

        // POST api/demo 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/demo/{id}
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/demo/5 
        public void Delete(int id)
        {
        }
    }
}