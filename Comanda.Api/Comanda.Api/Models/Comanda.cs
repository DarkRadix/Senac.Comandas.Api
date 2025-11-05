using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comanda.Api.Models
{
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();
    }
}
