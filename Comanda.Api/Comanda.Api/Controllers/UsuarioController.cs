using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // lista fixa de usuários
        static List<Usuario> usuarios = new List<Usuario> {
            new Usuario
            {
                Id = 1,
                Nome = "Admin",
                Email = "admin@admin.com",
                Senha = "admin"
            },
            new Usuario
            {
                Id = 2,
                Nome = "Usuario",
                Email = "usuario@usuario.com",
                Senha = "usuario"
            }
        };
        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = usuarios.
                FirstOrDefault(u => u.Id == id);
            if(usuario is null)
                return Results.NotFound("Usuário não encontrado!");

            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody]Usuario usuario)
        {
            // adicionar o usuário na lista
            usuarios.Add(usuario);

            return Results.Created($"/api/usuario/{usuario.Id }", usuario);

        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
