using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Comanda.Api.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }
}
