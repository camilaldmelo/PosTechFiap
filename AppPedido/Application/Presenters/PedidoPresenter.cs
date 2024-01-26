using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;
using Application.Interface.UseCases;

namespace Application.Presenters
{
    public class PedidoPresenter : IPedidoPresenter
    {
        public readonly IPedidoUseCase _pedidoUseCase;
        private readonly IMapper _mapper;

        public PedidoPresenter(IMapper mapper, IPedidoUseCase pedidoUseCase)
        {
            _pedidoUseCase = pedidoUseCase;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoViewModel>> ConvertToListViewModel(IEnumerable<Pedido> pedidos)
        {
            return await Task.Run(() => _mapper.Map<List<PedidoViewModel>>(pedidos));
        }

        public async Task<PedidoViewModel> ConvertToViewModel(Pedido pedido)
        {
            return await Task.Run(() => _mapper.Map<PedidoViewModel>(pedido));
        }

        public async Task<IEnumerable<Pedido>> ConvertFromListViewModel(IEnumerable<PedidoViewModel> pedidos)
        {
            return await Task.Run(() => _mapper.Map<List<Pedido>>(pedidos));
        }

        public async Task<Pedido> ConvertFromViewModel(PedidoViewModel pedido)
        {
            return await Task.Run(() => _mapper.Map<Pedido>(pedido));
        }

        public async Task<Tuple<Cliente, List<ProdutosPedido>>> ConvertFromViewModelForCreate(PedidoIncViewModel pedidoViewModel)
        {
            var cliente = _mapper.Map<Cliente>(pedidoViewModel);
            var produtosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return await Task.Run(() => new Tuple<Cliente, List<ProdutosPedido>>(cliente, produtosPedido));
        }

        public async Task<Pedido> ConvertFromViewModelForUpdate(PedidoIncViewModel pedidoViewModel)
        {
            return await Task.Run(() =>
            {
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                pedido.ProdutosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);
                return pedido;
            });
        }
    }
}
