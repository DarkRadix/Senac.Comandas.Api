using Microsoft.EntityFrameworkCore;

namespace Comanda.Api
{
    public class ComandasDBContext : DbContext
    {
        public ComandasDBContext(
            DbContextOptions<ComandasDBContext> options
        ) : base(options)
        {     }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Usuario>()
                .HasData(
                new Models.Usuario
                {
                    Id = 1,
                    Nome = "Admin",
                    Email = "admin@admin",
                    Senha = "admin123"
                }
            );
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.CardapioItem>();
        

            modelBuilder.Entity<Models.Mesa>()
                .HasData(
                new Models.Mesa
                    {
                        Id = 1,
                    NumeroMesa = 1,
                    SituacaoMesa = 0
                    },
                 new Models.Mesa
                    {
                        Id = 2,
                     NumeroMesa = 2,
                     SituacaoMesa = 0
                    },
                    new Models.Mesa
                    {
                        Id = 3,
                        NumeroMesa = 3,
                        SituacaoMesa = 0
                    }
                );

            modelBuilder.Entity<Models.CardapioItem>()
                .HasData(
                new Models.CardapioItem
                    {
                        Id = 1,
                    Descricao = "Hambúrguer clássico com queijo, alface, tomate e molho especial.",
                    Titulo = "Hambúrguer Clássico",
                    Preco = 20,
                    PossuiPreparo = true
                    },
                    new Models.CardapioItem
                    {
                        Id = 2,
                        Descricao = "Refrigerante gelado de 350ml para acompanhar sua refeição.",
                        Titulo = "Refrigerante 350ml",
                        Preco = 5,
                        PossuiPreparo = false
                    },
                    new Models.CardapioItem
                    {
                        Id = 3,
                        Descricao = "Porção de batatas fritas crocantes e douradas.",
                        Titulo = "Batatas Fritas",
                        Preco = 10,
                        PossuiPreparo = true
                    }
                );
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);



        }

        public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
        public DbSet<Models.Mesa> Mesas { get; set; } = default!;
        public DbSet<Models.Reserva> Reservas { get; set; } = default!;
        public DbSet<Models.Comanda> Comandas { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandaItems { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> PedidoCozinhas{ get; set; } = default!;
        public DbSet<Models.PedidoCozinhaItem> PedidoCozinhaItens { get; set; } = default!;
        public DbSet<Models.CardapioItem> CardapioItems { get; set; } = default!;

    }
}
