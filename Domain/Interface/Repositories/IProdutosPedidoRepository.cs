using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IProdutosPedidoRepository
    {
        public Task<IEnumerable<ProdutosPedido>> ObterProdutoPedidoPorPedido(int idPedido);

        public Task<bool> InserirProdutoPedido(ProdutosPedido produtosPedido);

        public Task<bool> DeletarProdutoPedidoPorIdPedido(int idPedido);
    }
}
