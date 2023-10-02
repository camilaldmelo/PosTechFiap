using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IProdutosPedidoRepository
    {
        public IEnumerable<ProdutosPedido> ObterProdutoPedidoPorPedido(int idPedido);

        public int InserirProdutoPedido(ProdutosPedido produtosPedido);

        public bool DeletarProdutoPedidoPorIdPedido(int idPedido);
    }
}
