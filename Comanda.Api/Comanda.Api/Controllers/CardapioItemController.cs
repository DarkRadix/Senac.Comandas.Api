using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItemController : ControllerBase
    {
        // GET: api/<CardapioItemController>
        [HttpGet]
        public IEnumerable<CardapioItem> Get()
        {
            return new CardapioItem[]
            {
              new CardapioItem {
                Id = 1,
                Titulo = "Coca-Cola",
                Descricao = "Refrigerante 350ml",
                Preco = 5.00m,
                PossuiPreparo = false

              },
              
              new CardapioItem {
                Id = 2,
                Titulo = "Xis Calota",
                Descricao = "Xis gigante de algo",
                Preco = 20m,
                PossuiPreparo = true

              }
            };
        }

    
       


        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
