namespace Application.ViewModel
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public required List<ProdutosPedidoViewModel> ProdutosPedido { get; set; }
    }
}
