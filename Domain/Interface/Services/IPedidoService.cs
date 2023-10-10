using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IPedidoService
    {
        public Task<IEnumerable<Pedido>> GetPedido(int idPedido); 

        public Task<bool> PutPedido(Pedido pedido);

        public Task<int> PostPedido(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido);
    }
}
