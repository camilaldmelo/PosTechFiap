using Application.Presenters.ViewModel;
using Domain.Entities;

namespace Application.Interface.Presenters
{
    public interface IPedidoPresenter
    {
        public Task<IEnumerable<PedidoViewModel>> ConvertToListViewModel(IEnumerable<Cliente> pedidos);

        public Task<PedidoViewModel> ConvertToViewModel(Pedido pedido);

        public Task<IEnumerable<Pedido>> ConvertFromListViewModel(IEnumerable<PedidoViewModel> pedidos);

        public Task<Pedido> ConvertFromViewModel(PedidoViewModel pedido);

        public Task<Pedido> ConvertFromViewModelForUpdate(PedidoIncViewModel pedidoViewModel);

        public Task<Tuple<Cliente, List<ProdutosPedido>>> ConvertFromViewModelForCreate(PedidoIncViewModel pedidoViewModel);
    }
}

