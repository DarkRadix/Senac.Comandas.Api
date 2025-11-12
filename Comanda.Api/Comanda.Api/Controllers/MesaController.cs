using System.Runtime.CompilerServices;
using Comanda.Api.DTOs;
using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly ComandasDBContext _context;

        public MesaController(ComandasDBContext context)
        {
            _context = context;
        }

        // GET: api/<MesaController>
        [HttpGet]
        public IResult GetMesa()
        {
            var mesas = _context.Mesas.ToList();
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            {
                return Results.NotFound("Mesa não encontrada!");
            }
            return Results.Ok(mesa);
        }

        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody]MesaCreateRequest mesaCreate)
        {
            if (mesaCreate.NumeroMesa <= 0)
            {
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            }
            var mesa = new Mesa
            {
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = (int)SituacaoMesa.Livre
            };
            _context.Mesas.Add(mesa);
            _context.SaveChanges();
            return Results.Created($"/api/mesa/{mesa.Id}", mesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody]MesaUpdateRequest mesaUpdate)
        {
            var mesa = _context.Mesas.
                FirstOrDefault(m => m.Id == id);
            if (mesa is null)
                return Results.NotFound("Mesa não encontrada!");
            if (mesaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (mesaUpdate.SituacaoMesa < 0 || mesaUpdate.SituacaoMesa > 2)
                return Results.BadRequest("A situação da mesa deve ser 0 (Livre), 1 (Ocupada) ou 2 (Reservado).");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            _context.SaveChanges();
            return Results.Ok(mesa);
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = _context.Mesas.
                FirstOrDefault(m => m.Id == id);
            if (mesa is not null)
            return Results.NotFound($"Mesa {id} não encontrada!");
            var removido = _context.SaveChanges();
            if (removido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
