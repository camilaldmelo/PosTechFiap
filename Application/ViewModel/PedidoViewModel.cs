namespace Application.ViewModel
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public required List<ProdutosPedidoViewModel> ProdutosPedido { get; set; }
    }
}
