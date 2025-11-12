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
        public  ComandasDBContext _context { get; set; }
        //construtor
        public UsuarioController(ComandasDBContext context) 
        {
            _context = context;
        }
       
       
        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            //conecta no banco e traz a lista de usuarios(SELECT * FROM USUARIOS)
            var usuarios = _context.Usuarios.ToList();
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = _context.Usuarios.
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
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };
            // adicionar o usuário na lista
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Results.Created($"/api/usuario/{usuario.Id }", usuario);

        }
        //ATUALIZA UM USUARIO
        // PUT api/<UsuarioController>/5




        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioupdate)
        {
            var usuario = _context.Usuarios.
                FirstOrDefault(u => u.Id == id);
            if(usuario is null)
            
                return Results.NotFound($"Usuario do id {id} nao encontrado.");
            if (usuarioupdate.Nome.Length < 3)
                return Results.BadRequest("O nome deve ter no mínimo 3 caracteres.");
            if (usuarioupdate.Email.Length < 6)
                return Results.BadRequest("O email deve ser valido");
            if (usuarioupdate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter no mínimo 6 caracteres.");
            usuario.Nome = usuarioupdate.Nome;
            usuario.Email = usuarioupdate.Email;
            usuario.Senha = usuarioupdate.Senha;
            _context.SaveChanges();
            return Results.NoContent();

            // retorno no content
            return Results.NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound($"Usuário {id} não encontrado");
            _context.Usuarios.Remove(usuario);
            var removido = _context.SaveChanges();
            if (removido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);


        }
    }
}
