using Comanda.Api.DTOs;
using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
       private readonly ComandasDBContext _context;

        public ComandaController(ComandasDBContext context)
        {
            _context = context;
        }

        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(_context.Comandas.ToList());
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda não encontrada");
            return Results.Ok(comanda);
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
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa
            };
            var comandaItens = new List<ComandaItem>();

            foreach (int cardapioItemId in comandaCreate.CardapioItemsIds)
            {
               var comandaItemCardapio = new ComandaItem
                {
                    CardapioItemId = cardapioItemId,
                    Comanda = newComanda,
                };
                comandaItens.Add(comandaItemCardapio);
                //Criar o Pedido de Cozinha de acordo com cadastro do cardapio possuiprepreparo
                var cardapioItem = _context.CardapioItems
                    .FirstOrDefault(c => c.Id == cardapioItemId);
                if(cardapioItem!.PossuiPreparo)
                {
                    var pedido = new PedidoCozinha
                    {
                        Comanda = newComanda

                    };
                    var pedidoItem = new PedidoCozinhaItem
                    {
                        ComandaItem = comandaItemCardapio,
                        PedidoCozinha = pedido
                    };
                    _context.PedidoCozinhas.Add(pedido);
                    _context.PedidoCozinhaItens.Add(pedidoItem);
                }
            }
            newComanda.Itens = comandaItens;
            _context.Comandas.Add(newComanda);
            _context.SaveChanges();
            var response = new ComandaCreateResponse
            {
                Id = newComanda.Id,
                NomeCliente = newComanda.NomeCliente,
                NumeroMesa = newComanda.NumeroMesa,
                Itens = newComanda.Itens.Select(i => new ComandaItemResponse
                {
                    Id = i.Id,
                    Titulo = _context.CardapioItems
                    .First(ci => ci.Id == i.CardapioItemId).Titulo
                }).ToList()
            };
            return Results.Created($"/api/comanda/{response.Id}", response);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]

        public IResult Put(int id, [FromBody]ComandaUpdateRequest comandaUpdate)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
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
           foreach(var item in comandaUpdate.Itens)
            {
                if(item.Id > 0 && item.Remove == true)
                {
                    RemoverItemComanda(item.Id);
                }
                if(item.CardapioItemId>0)
                {
                    InserirItemComanda(comanda, item.CardapioItemId);
                }


            }
            _context.SaveChanges();
            return Results.NoContent();

        }

        private void InserirItemComanda(Models.Comanda comanda, int cardapioItemId)
        {
            _context.ComandaItems.Add(
                new ComandaItem
                {
                    CardapioItemId = cardapioItemId,
                    Comanda = comanda
                }

                );
        }

        private void RemoverItemComanda(int id)
        {
            var comandaItem = _context.ComandaItems.FirstOrDefault(ci => ci.Id == id);
            if(comandaItem is not null)
            {
                _context.ComandaItems.Remove(comandaItem);
            }
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound($"Comanda {id} não encontrado");
            _context.Comandas.Remove(comanda);
            var removidoComSucesso = _context.SaveChanges();
            if(removidoComSucesso>0)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
