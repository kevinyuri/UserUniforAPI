using Microsoft.EntityFrameworkCore;
using UserUniforAPI.Data;
using UserUniforAPI.Model;

namespace UserUniforAPI.Services
{
    public static class PedidoService
    {
        public static void MapAPPEndpointPedido(this WebApplication app)
        {
            // Endpoints para Pedidos
            app.MapGet("v1/pedidos", async (AppDbContext context) =>
            {
                try
                {
                    var pedidos = await context.Pedidos
                        .Include(p => p.PedidoProdutos)
                        .ThenInclude(pp => pp.Produto)
                        .ToListAsync();
                    return Results.Ok(pedidos);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest($"{ex.Message}");
                }
            });

            app.MapGet("v1/pedidos/{id}", async (AppDbContext context, int id) =>
            {
                var pedido = await context.Pedidos
                    .Include(p => p.PedidoProdutos)
                    .ThenInclude(pp => pp.Produto)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pedido != null)
                {
                    return Results.Ok(pedido);
                }
                else
                {
                    return Results.NotFound($"Pedido com o ID {id} não encontrado.");
                }
            });

            app.MapPost("v1/pedidos", async (AppDbContext context, PedidoCreate novoPedido) =>
            {
                // Criando o novo pedido com base nos dados da requisição
                var pedidoAdicionar = new Pedido
                {
                    DataPedido = novoPedido.DataPedido,
                    UsuarioId = novoPedido.UsuarioId,
                };

                // Adicionando o pedido ao banco de dados
                context.Pedidos.Add(pedidoAdicionar);
                await context.SaveChangesAsync();

                // Associe os produtos ao pedido, usando os IDs dos produtos fornecidos
                foreach (var produtoId in novoPedido.ProdutoIds)
                {
                    var produto = await context.Produtos.FindAsync(produtoId);
                    if (produto != null)
                    {
                        var pedidoProduto = new PedidoProduto
                        {
                            PedidoId = pedidoAdicionar.Id,
                            ProdutoId = produto.Id,
                            Quantidade = 1 // Defina a quantidade conforme necessário
                        };
                        context.PedidoProdutos.Add(pedidoProduto);
                    }
                }

                // Salvando as mudanças associadas aos produtos no pedido
                await context.SaveChangesAsync();

                return Results.Created($"v1/pedidos/{pedidoAdicionar.Id}", pedidoAdicionar);
            });


            app.MapPut("v1/pedidos/{id}", async (AppDbContext context, int id, Pedido pedidoAtualizado) =>
            {
                var pedidoExistente = await context.Pedidos
                    .Include(p => p.PedidoProdutos)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pedidoExistente == null)
                {
                    return Results.NotFound();
                }

                pedidoExistente.PedidoProdutos = pedidoAtualizado.PedidoProdutos;
                pedidoExistente.DataPedido = pedidoAtualizado.DataPedido;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("v1/pedidos/{id}", async (AppDbContext context, int id) =>
            {
                var pedido = await context.Pedidos.FindAsync(id);
                if (pedido == null)
                {
                    return Results.NotFound();
                }

                context.Pedidos.Remove(pedido);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }

}
