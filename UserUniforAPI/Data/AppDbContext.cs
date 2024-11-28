using Microsoft.EntityFrameworkCore;
using UserUniforAPI.Model;

namespace UserUniforAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=tcp:demoprojects.database.windows.net,1433;Initial Catalog=usuarioUnifor;Persist Security Info=False;User ID=kevinsato;Password=#sw514132;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir o nome das tabelas no banco de dados, se necessário
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<PedidoProduto>().ToTable("PedidoProdutos");

            // Configurando o relacionamento entre Pedido e Produto
            modelBuilder.Entity<PedidoProduto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProdutoId });

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(pp => pp.PedidoId);

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Produto)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(pp => pp.ProdutoId);
        }
    }
}
