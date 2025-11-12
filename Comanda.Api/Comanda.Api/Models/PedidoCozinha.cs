using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comanda.Api.Models
{
    public class PedidoCozinha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public virtual Comanda Comanda { get; set; }
        public List<PedidoCozinhaItem> Itens { get; set; } = [];
    }
}
