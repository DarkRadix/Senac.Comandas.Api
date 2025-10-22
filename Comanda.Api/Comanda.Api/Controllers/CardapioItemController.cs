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
        static List<CardapioItem> cardapios = new List<CardapioItem>()
        {
            new CardapioItem
            {
                Id = 1,
                Descricao = "Coxinha de frango com catupiry",
                Preco = 5.50m,
                PossuiPreparo = true,
            },
            new CardapioItem
            {
                Id = 2,
                Descricao = "Pastel de carne com queijo",
                Preco = 7.00m,
                PossuiPreparo = true,
            }
        };

        // METODO GET que retorna a lista de cardapio
        // GET: api/<CardapioItemController>
        [HttpGet] // ANOTACAO QUE INDICA QUE ESSE METODO RESPONDE A REQUISICOES GET
        public IResult GetCardapio()
        {
            // CRIA UMA LISTA ESTATICA DE CARDAPIO E TRANSFORMA EM JSON
            return Results.Ok(cardapios);
        }


        // GET api/<CardapioItemController>/1
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // BUSCAR NA LISTA de cardapios de acorod com o Id do parametro
            //joga o valor para a variavel o primeiro elemento de acordo com o id
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound("Cardápio não encontrado!");
            }
            // retorna o valor para o ednpoint da api
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if (cardapio.Titulo.Length < 3)
            {
                return Results.BadRequest("O título do item do cardápio deve ter no mínimo 3 caracteres.");
            }
            if (cardapio.Descricao.Length < 5)
            {
                return Results.BadRequest("A descrição do item do cardápio deve ter no mínimo 5 caracteres.");
            }
            if (cardapio.Preco <= 0)
            {
                return Results.BadRequest("O preço do item do cardápio deve ser maior que zero.");
            }
            var cardapioItem = new CardapioItem
            {
                Id = cardapios.Count + 1,
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
            cardapios.Add(cardapioItem);
            return Results.Created($"/api/cardapioitem/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {

        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // buscar o cardapio na lista pelo id
            var cardapioItem = cardapios
                .FirstOrDefault(c => c.Id == id);
            // se estiver nulo, retorna 404
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            // remove o cardapio da lista
            var removidoComSucesso = cardapios.Remove(cardapioItem);
            // retorna 204 sem conteudo
            if (removidoComSucesso)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
