using Microsoft.EntityFrameworkCore;

namespace Comanda.Api
{
    public class ComandasDBContext : DbContext
    {
        public ComandasDBContext(
            DbContextOptions<ComandasDBContext> options
        ) : base(options)
        {   }
        public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
        public DbSet<Models.Mesa> Mesas { get; set; } = default!;
        public DbSet<Models.Reserva> Reservas { get; set; } = default!;
        public DbSet<Models.Comanda> Comandas { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandaItems { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> PedidoCozinhas { get; set; } = default!;
        public DbSet<Models.PedidoCozinhaItem> PedidoCozinhaItems { get; set; } = default!;
        public DbSet<Models.CardapioItem> CardapioItems { get; set; } = default!;

    }
}
