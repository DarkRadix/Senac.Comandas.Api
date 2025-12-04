using Comanda.Api.DTOs;
using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
        public readonly ComandasDBContext _context;
        public PedidoCozinhaController (ComandasDBContext context)    
        {
            _context = context;
        }
    

        // GET: api/<PedidoController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(_context.PedidoCozinhas.ToList());
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public IResult GetResult(int id)
        {
            var pedido = _context.PedidoCozinhas.FirstOrDefault(p=>p.Id == id);
            if (pedido is null)
                return Results.NotFound($"Pedido {id} não encontrado!");

            return Results.Ok(pedido);
        }

        // POST api/<PedidoController>
        [HttpPost]
        public IResult Post([FromBody]PedidoCozinhaCreateRequest pedidoCreate)
        {
            if (pedidoCreate.Itens == null || !pedidoCreate.Itens.Any())
                return Results.BadRequest("O pedido deve conter ao menos um item");
            if (pedidoCreate.ComandaId <= 0)
                return Results.BadRequest("ComandaId Invalido");
            var pedido = new PedidoCozinha
            {
                ComandaId = pedidoCreate.ComandaId,
            };
            var itens = new List<PedidoCozinhaItem>();
            foreach (var item in pedidoCreate.Itens)             {
                var pedidoItem = new PedidoCozinhaItem
                {
                    ComandaItemId = item.ComandaItemId,
                };
                itens.Add(pedidoItem);
            }
            pedido.Itens = itens;

            return Results.Created($"/api/pedidoCozinha/{pedido.Id}", pedido);
        }
       

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody]PedidoCozinhaUpdateRequest pedidoUpdate)
        {
            var pedido = _context.PedidoCozinhas.FirstOrDefault(p => p.Id == id);
            if (pedido is null)
                return Results.NotFound($"O pedido {id} não encontrado");

            if (pedidoUpdate.Itens == null || !pedidoUpdate.Itens.Any())
                return Results.BadRequest("O pedido deve conter ao menos um item.");

            pedido.ComandaId = pedidoUpdate.ComandaId;

            _context.SaveChanges();
            return Results.NoContent();

        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var pedido = _context.PedidoCozinhas.FirstOrDefault(p => p.Id == id);

            if (pedido is null)
                return Results.NotFound($"Pedido {id} não encontrado");

            _context.PedidoCozinhas.Remove(pedido);
            var pedidoRemovido = _context.SaveChanges();
            if (pedidoRemovido > 0)
                return Results.NoContent();
            return Results.StatusCode(500);

        }
    }
}
