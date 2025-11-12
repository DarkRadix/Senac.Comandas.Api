using Comanda.Api.DTOs;
using Comanda.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    //CRIA A ROTA DO COTROLADOR
    [Route("api/[controller]")]
    [ApiController] // DEFINE QUE ESSA CLASSE É UM CONTROLADOR DE API
    public class CardapioItemController : ControllerBase // HERDA DE CONTROLLERBASE para PODER RESPONDER A REQUISICOES HTTP
    {

        private readonly ComandasDBContext _context;
        public CardapioItemController(ComandasDBContext context)
        {
            _context = context;
        }
       
        // GET: api/<CardapioItemController>
        [HttpGet] // ANOTACAO QUE INDICA QUE ESSE METODO RESPONDE A REQUISICOES GET
        public IResult GetCardapio()
        {
            var cardapios = _context.CardapioItems.ToList();
            return Results.Ok(cardapios);
        }


        // GET api/<CardapioItemController>/1
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // BUSCAR NA LISTA de cardapios de acorod com o Id do parametro
            //joga o valor para a variavel o primeiro elemento de acordo com o id
            var cardapio = _context.CardapioItems.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound("Cardápio não encontrado!");
            }
            // retorna o valor para o ednpoint da api
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody]CardapioItemCreateRequest cardapio)
        {
            if(cardapio.Titulo.Length < 3)
            {
                Results.BadRequest("O título deve ter no mínimo 3 caracteres.");
            }
            if(cardapio.Descricao.Length < 5)
            {
                Results.BadRequest("A descrição deve ter no mínimo 5 caracteres.");
            }
            if(cardapio.Preco <= 0)
            {
                Results.BadRequest("O preço deve ser maior que zero.");
            }
            var cardapioItem = new CardapioItem
            {
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
            _context.CardapioItems.Add(cardapioItem);
            _context.SaveChanges();
            return Results.Created($"/api/cardapio/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        /// <summary>
        /// Atualiza um item do cardápio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardapio"></param>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody]CardapioItemUpdateRequest cardapio)
      
        {
           var cardapioItem = _context.CardapioItems.
                FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
            return Results.NotFound($"Usuario do id {id} nao encontrado.");


            cardapioItem.Titulo = cardapio.Titulo;
            cardapioItem.Descricao = cardapio.Descricao;
            cardapioItem.Preco = cardapio.Preco;
            cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            _context.SaveChanges();
            return Results.NoContent();

        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var cardapioItem = _context.CardapioItems.
                FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null) ;
            return Results.NotFound($"Cardapio {id} não encontrado");
            _context.CardapioItems.Remove(cardapioItem);
            var removido = _context.SaveChanges();
            if (removido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
