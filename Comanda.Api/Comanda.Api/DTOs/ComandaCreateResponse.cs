using Comanda.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comanda.Api.DTOs
{
    public class ComandaCreateResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public List<ComandaItemResponse> Itens { get; set; } = new List<ComandaItemResponse>();
    }
    public class ComandaItemResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
    }
}
