namespace Application.Presenters.ViewModel
{
    public class PedidoIncViewModel
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }

        public required List<ProdutosPedidoIncViewModel> ProdutosPedido { get; set; }
    }
}
