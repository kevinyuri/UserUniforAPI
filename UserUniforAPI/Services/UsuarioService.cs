using UserUniforAPI.Data;
using UserUniforAPI.Model;

namespace UserUniforAPI.Services
{
    public static class UsuarioService
    {
        public static void MapAPPEndpoint(this WebApplication app)
        {
            app.MapGet("v1/usuarios", (AppDbContext context) =>
            {
                try
                {
                    var usuarios = context.Usuarios.ToList();
                    return Results.Ok(usuarios);
                }
                catch (Exception ex)
                {

                    return Results.BadRequest($"{ex.Message}");
                }
            });

            app.MapGet("v1/usuarios/{id}", (AppDbContext context, int id) =>
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);

                if (usuario != null)
                {
                    return Results.Ok(usuario);
                }
                else
                {
                    return Results.NotFound($"Usuário com o ID {id} não encontrado.");
                }
            });

            app.MapPost("v1/usuarios", async (AppDbContext context, UsuarioCreate novoUsuario) =>
            {
                var usuarioAdicionar = new Usuario
                {
                    Nome = novoUsuario.Nome,
                    Sobrenome = novoUsuario.Sobrenome,
                    senha = novoUsuario.senha,
                    userName = novoUsuario.userName,
                };

                context.Usuarios.Add(usuarioAdicionar);
                await context.SaveChangesAsync();
                return Results.Created($"v1/usuarios/{usuarioAdicionar.Id}", usuarioAdicionar);
            });

            app.MapPut("v1/usuarios/{id}", async (AppDbContext context, int id, UsuarioCreate usuarioAtualizado) =>
            {
                var usuarioExistente = await context.Usuarios.FindAsync(id);
                if (usuarioExistente is null)
                {
                    return Results.NotFound();
                }

                usuarioExistente.Nome = usuarioAtualizado.Nome;
                usuarioExistente.Sobrenome = usuarioAtualizado.Sobrenome;
                usuarioExistente.userName = usuarioAtualizado.userName;
                usuarioExistente.senha = usuarioAtualizado.senha;


                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("v1/usuarios/{id}", async (AppDbContext context, int id) =>
            {
                var usuario = await context.Usuarios.FindAsync(id);
                if (usuario is null)
                {
                    return Results.NotFound();
                }

                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
