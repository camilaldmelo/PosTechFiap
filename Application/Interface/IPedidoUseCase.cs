using Application.ViewModel;

namespace Application.Interface
{
    public interface IPedidoUseCase
    {
        public Task<PedidoViewModel> GetById(int idPedido);

        public Task<IEnumerable<PedidoViewModel>> GetByIdStatus(int idAcompanhamento);

        public Task<bool> Update(PedidoIncViewModel pedido);

        public Task<bool> UpdateStatus(int idPedido, int idStatus);

        public Task<int> Create(PedidoIncViewModel pedido);
    }
}
