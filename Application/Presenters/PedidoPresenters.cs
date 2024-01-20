using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;
using Application.Interface.UseCases;

namespace Application.Presenters
{
    public class PedidoPresenters : IPedidoPresenters
    {
        public IPedidoUseCases _pedidoService;
        private readonly IMapper _mapper;

        public PedidoPresenters(IMapper mapper, IPedidoUseCases pedidoService)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        public async Task<PedidoViewModel> GetById(int idPedido)
        {
            var pedido = await _pedidoService.GetById(idPedido);

            return _mapper.Map<PedidoViewModel>(pedido);
        }

        public async Task<IEnumerable<PedidoViewModel>> GetByIdStatus(int idAcompanhamento)
        {
            var pedido = await _pedidoService.GetByIdStatus(idAcompanhamento);

            return _mapper.Map<List<PedidoViewModel>>(pedido);
        }

        public async Task<IEnumerable<PedidoViewModel>> GetInProgress()
        {
            var pedidos = await _pedidoService.GetInProgress();

            return _mapper.Map<List<PedidoViewModel>>(pedidos);
        }

        public async Task<int> Create(PedidoIncViewModel pedidoViewModel)
        {
            var cliente = _mapper.Map<Cliente>(pedidoViewModel);
            var produtosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return await _pedidoService.Create(cliente, produtosPedido);
        }

        public async Task<bool> UpdateStatus(int idPedido, int idStatus)
        {
            return await _pedidoService.UpdateStatus(idPedido, idStatus);
        }

        public async Task<bool> Update(PedidoIncViewModel pedidoViewModel)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            pedido.ProdutosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return await _pedidoService.Update(pedido);
        }
    }
}
