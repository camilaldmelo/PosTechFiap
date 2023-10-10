using Application.ViewModel;

namespace Application.Interface
{
    public interface IPedidoUseCase
    {
        public Task<IEnumerable<PedidoViewModel>> GetPedido(int idPedido);

        public Task<bool> PutPedido(PedidoViewModel pedido);

        public Task<int> PostPedido(PedidoViewModel pedido);
    }
}
