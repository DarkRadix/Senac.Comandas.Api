using Comanda.Api.DTOs;
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
        public IResult Post([FromBody]UsuarioCreateRequest usuarioCreate)
        {
            if(usuarioCreate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter no mínimo 6 caracteres.");
            if (usuarioCreate.Nome.Length < 3)
                return Results.BadRequest("O nome deve ter no mínimo 3 caracteres.");
            if (usuarioCreate.Email.Length < 6)
                return Results.BadRequest("O email deve ser valido");
            var usuario = new Usuario
            {
                Id = usuarios.Count + 1,
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };
            // adicionar o usuário na lista
            usuarios.Add(usuario);

            return Results.Created($"/api/usuario/{usuario.Id }", usuario);

        }
        //ATUALIZA UM USUARIO
        // PUT api/<UsuarioController>/5




        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioupdate)
        {
            var usuario = usuarios.
                FirstOrDefault(u => u.Id == id);
            if(usuario is null)
            
                return Results.NotFound($"Usuario do id {id} nao encontrado.");

            CardapioItem.Titulo = cardapio.Titulo;
            CardapioItem.Descricao = cardapio.Descricao;
            CardapioItem.Preco = cardapio.Preco;
            CardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            return Results.NoContent();

        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
