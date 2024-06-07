using Microsoft.EntityFrameworkCore;
using UserUniforAPI.Model;

namespace UserUniforAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=tcp:demoprojects.database.windows.net,1433;Initial Catalog=usuarioUnifor;Persist Security Info=False;User ID=kevinsato;Password=#sw514132;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}
