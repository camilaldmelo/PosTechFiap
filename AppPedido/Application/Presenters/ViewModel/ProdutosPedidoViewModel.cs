namespace Application.Presenters.ViewModel
{
    public class ProdutosPedidoViewModel
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }

        public ProdutoViewModel Produto { get; set; }
    }
}
