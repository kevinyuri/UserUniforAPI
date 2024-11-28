namespace UserUniforAPI.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }

        // Relacionamento com pedidos (um produto pode estar em vários pedidos)
        public ICollection<PedidoProduto> PedidoProdutos { get; set; }
    }

    public class ProdutoCreate
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
    }

}
