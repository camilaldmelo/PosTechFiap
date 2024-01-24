namespace Application.Presenters.ViewModel
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get { return ProdutosPedido.Sum(pp => pp.Produto.Preco * pp.Quantidade); } }

        public AcompanhamentoViewModel? Acompanhamento { get; set; }
        public ClienteViewModel? Cliente { get; set; }

        public required List<ProdutosPedidoViewModel> ProdutosPedido { get; set; }
    }
}
