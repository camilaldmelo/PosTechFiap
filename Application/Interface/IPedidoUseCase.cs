using Application.ViewModel;

namespace Application.Interface
{
    public interface IPedidoUseCase
    {
        public Task<IEnumerable<PedidoViewModel>> GetById(int idPedido);

        public Task<bool> Update(PedidoViewModel pedido);

        public Task<int> Create(PedidoViewModel pedido);
    }
}
