using Microsoft.EntityFrameworkCore;
using UserUniforAPI.Data;
using UserUniforAPI.Model;

namespace UserUniforAPI.Services
{
    public static class ProdutoService
    {
        public static void MapAPPEndpointProduct(this WebApplication app)
        {
            // Endpoints para Produtos
            app.MapGet("v1/produtos", async (AppDbContext context) =>
            {
                try
                {
                    var produtos = await context.Produtos.ToListAsync();
                    return Results.Ok(produtos);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest($"{ex.Message}");
                }
            });

            app.MapGet("v1/produtos/{id}", async (AppDbContext context, int id) =>
            {
                var produto = await context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

                if (produto != null)
                {
                    return Results.Ok(produto);
                }
                else
                {
                    return Results.NotFound($"Produto com o ID {id} não encontrado.");
                }
            });

            app.MapPost("v1/produtos", async (AppDbContext context, ProdutoCreate novoProduto) =>
            {
                // Criando o novo produto
                var produtoAdicionar = new Produto
                {
                    Nome = novoProduto.Nome,
                    Preco = novoProduto.Preco,
                    QuantidadeEmEstoque = novoProduto.QuantidadeEmEstoque
                };

                // Adicionando o produto ao banco de dados
                context.Produtos.Add(produtoAdicionar);
                await context.SaveChangesAsync();

                // Retornando a resposta com o novo produto criado
                return Results.Created($"v1/produtos/{produtoAdicionar.Id}", produtoAdicionar);
            });


            app.MapPut("v1/produtos/{id}", async (AppDbContext context, int id, Produto produtoAtualizado) =>
            {
                var produtoExistente = await context.Produtos.FindAsync(id);
                if (produtoExistente == null)
                {
                    return Results.NotFound();
                }

                produtoExistente.Nome = produtoAtualizado.Nome;
                produtoExistente.Preco = produtoAtualizado.Preco;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("v1/produtos/{id}", async (AppDbContext context, int id) =>
            {
                var produto = await context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    return Results.NotFound();
                }

                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }

}
