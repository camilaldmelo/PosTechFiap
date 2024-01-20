using Application.Presenters.ViewModel;

namespace Application.Interface.Presenters
{
    public interface IPedidoPresenters
    {
        public Task<PedidoViewModel> GetById(int idPedido);

        public Task<IEnumerable<PedidoViewModel>> GetByIdStatus(int idAcompanhamento);

        public Task<IEnumerable<PedidoViewModel>> GetInProgress();

        public Task<bool> Update(PedidoIncViewModel pedido);

        public Task<bool> UpdateStatus(int idPedido, int idStatus);

        public Task<int> Create(PedidoIncViewModel pedido);
    }
}
