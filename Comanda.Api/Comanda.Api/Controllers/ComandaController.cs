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
                Itens = new List<ComandaItem>
                {
                    new ComandaItem
                    {
                        Id = 1, 
                        CardapioItemId = 1, 
                        ComandaId = 1,
                    },
                    new ComandaItem
                    {
                        Id = 2,
                        CardapioItemId = 2,
                        ComandaId = 1,
                    }
                }
            },
            new Models.Comanda()
            {
                Id = 1,
                NomeCliente = "Bruno",
                NumeroMesa = 2,
                Itens = new List<ComandaItem>
                {
                    new ComandaItem
                    {
                        Id = 3,
                        CardapioItemId = 1,
                        ComandaId = 1,
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
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            if (comandaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (comandaCreate.CardapioItemsIds.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos um item do cardápio.");
            var novacomanda = new Models.Comanda
            {
                Id = comandas.Count + 1,
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa
            };
            // cria uma variavel do tipo lista de itens
            var comandaItens = new List<ComandaItem>();
            // percorre os ids dos itens do cardapio
            foreach (int cardaItemId in comandaCreate.CardapioItemsIds)
            {
                // cria um novo item da comanda
                var comandaItem = new ComandaItem
                {
                    Id = comandaItens.Count + 1,
                    CardapioItemId = cardaItemId,
                    ComandaId = novacomanda.Id
                };
                // adiciona o itens na lista de itens   
                comandaItens.Add(comandaItem);
            }

            // atribui os itens a nota comanda
            comandas.Add(novacomanda);
            // adiciona a nova comanda na lista de comandas
            return Results.Created($"/api/comanda/{novacomanda.Id}", novacomanda);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            // pesquisa uma comanda na lista de comandas pelo id da comanda que veio no parametro da request
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null) // se não encontrou a comanda pesquisada
            // retorna um codigo 404 Não encontrado
            return Results.NotFound("Comanda não encontrada");
            // validar o nome do cliente
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            // validar o numero da mesa
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");

            // Atualia os dados da comanda
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;

            // retorna 204 Sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
