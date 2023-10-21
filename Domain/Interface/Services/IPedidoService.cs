using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IPedidoService
    {
        public Task<IEnumerable<Pedido>> GetById(int idPedido); 

        public Task<bool> Update(Pedido pedido);

        public Task<int> Create(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido);
    }
}
