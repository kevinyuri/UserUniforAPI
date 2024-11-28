namespace UserUniforAPI.Model
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }

        // Relacionamento com o usuário (um pedido pertence a um único usuário)
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relacionamento com produtos (um pedido pode ter vários produtos)
        public ICollection<PedidoProduto> PedidoProdutos { get; set; }
    }

    public class PedidoCreate
    {
        // Data do pedido - não precisa ser informado, pode ser gerada automaticamente no servidor
        public DateTime DataPedido { get; set; } = DateTime.Now;

        // ID do usuário - quem está criando o pedido
        public int UsuarioId { get; set; }

        // Lista de produtos - os produtos que serão adicionados ao pedido
        public List<int> ProdutoIds { get; set; } // IDs dos produtos, será usado para associar os produtos ao pedido
    }

}
