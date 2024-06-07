using Microsoft.EntityFrameworkCore;
using UserUniforAPI.Model;

namespace UserUniforAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=LAPTOP-0AKONMPE;Database=UsuariosUnifor;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
