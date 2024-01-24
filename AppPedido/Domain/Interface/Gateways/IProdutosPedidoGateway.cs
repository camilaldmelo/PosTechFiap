using Domain.Entities;

namespace Domain.Interface.Gateways
{
    public interface IProdutosPedidoGateway
    {
        public Task<IEnumerable<ProdutosPedido>> GetByIdPedido(int idPedido);

        public Task<bool> Create(ProdutosPedido produtosPedido);

        public Task<bool> DeleteByIdPedido(int idPedido);
    }
}
