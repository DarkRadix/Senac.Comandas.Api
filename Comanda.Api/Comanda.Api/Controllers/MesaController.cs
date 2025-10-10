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
        public List<Mesa> mesas = new List<Mesa>()
        {
            new Mesa
            {
                Id = 1,
                NumeroMesa = 1,
                SituacaoMesa = (int)SituacaoMesa.Livre
            },
            new Mesa
            {
                Id = 2,
                NumeroMesa = 2,
                SituacaoMesa = (int)SituacaoMesa.Ocupada

            }
        };

        // GET: api/<MesaController>
        [HttpGet]
        public IResult GetMesa()
        {
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            {
                return Results.NotFound("Mesa não encontrada!");
            }
            return Results.Ok(mesas);
        }

        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody]MesaCreateRequest mesaCreate)
        {
            if (mesaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            var mesa = new Mesa
            {
                Id = mesas.Count + 1,
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = (int)SituacaoMesa.Livre
            };
            mesas.Add(mesa);
            return Results.Created($"/api/mesa/{mesa.Id}", mesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody]MesaUpdateRequest mesaUpdate)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
                return Results.NotFound("Mesa não encontrada!");
            if (mesaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (mesaUpdate.SituacaoMesa < 0 || mesaUpdate.SituacaoMesa > 2)
                return Results.BadRequest("A situação da mesa deve ser 0 (Livre), 1 (Ocupada) ou 2 (Reservado).");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            return Results.Ok(mesa);
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
