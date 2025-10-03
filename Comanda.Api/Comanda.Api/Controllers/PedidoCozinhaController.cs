using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
        List<PedidoCozinha> pedidosCozinha = new List<PedidoCozinha>() 
        { 
        new PedidoCozinha()
        {
            Id = 1,
            ComandaId = 1
        },
        new PedidoCozinha()
        {
            Id = 2,
            ComandaId = 2
        }
    };

        // GET: api/<PedidoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PedidoController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
