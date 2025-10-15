using Comanda.Api.DTOs;
using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        static List<Models.Comanda> comandas = new List<Models.Comanda>()
        {
            new Models.Comanda()
            {
                Id = 1,
                NomeCliente = "Ana",
                NumeroMesa = 1,
                Itens = new List<ComandaItem>()
                {
                    new ComandaItem()
                    {
                        Id = 1,
                        CardapioItemId = 1,
                        ComandaId = 1
                    },
                   
                }
            },
            new Models.Comanda()
            {
                Id = 2,
                NomeCliente = "Bruno",
                NumeroMesa = 2,
                Itens = new List<ComandaItem>()
                {
                 new ComandaItem()
                    {
                        Id = 2,
                        CardapioItemId = 2,
                        ComandaId = 1
                    }
                }
            }
        };

        // GET: api/<ComandaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody]ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            if (comandaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if(comandaCreate.CardapioItemsIds.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos um item do cardápio.");
            var newComanda = new Models.Comanda
            {
                Id = comandas.Count + 1,
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa
            };
            var comandaItens = new List<ComandaItem>();
            foreach (int cardapioItemId in comandaCreate.CardapioItemsIds)
            {
                var comandaItemId = comandaItens.Count + 1;
                var comandaItemCardapio = new ComandaItem
                {
                    Id = comandaItemId,
                    CardapioItemId = cardapioItemId,
                    ComandaId = newComanda.Id
                };
                comandaItens.Add(comandaItemCardapio);
               
            }

            comandas.Add(newComanda);
            return Results.Created($"/api/comanda/{newComanda.Id}", newComanda);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]

        public IResult Put(int id, [FromBody]ComandaUpdateRequest comandaUpdate)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if(comanda is null)
                return Results.NotFound("Comanda não encontrada!");
            if(comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");

            //atualiza as informações da comanda
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            //retorna 204 sem conteúdo
            return Results.NoContent();

        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
