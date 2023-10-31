using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IProdutosPedidoRepository
    {
        public Task<IEnumerable<ProdutosPedido>> GetByIdPedido(int idPedido);

        public Task<bool> Create(ProdutosPedido produtosPedido);

        public Task<bool> DeleteByIdPedido(int idPedido);
    }
}
