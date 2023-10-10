using Application.Interface;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;

namespace Application.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        public IPedidoService _pedidoService;
        private readonly IMapper _mapper;

        public PedidoUseCase(IMapper mapper, IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoViewModel>> GetPedido(int idPedido)
        {
            var pedido = await _pedidoService.GetPedido(idPedido);

            return _mapper.Map<List<PedidoViewModel>>(pedido);
        }

        public async Task<int> PostPedido(PedidoViewModel pedidoViewModel)
        {
            var cliente = _mapper.Map<Cliente>(pedidoViewModel);
            var produtosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return await _pedidoService.PostPedido(cliente, produtosPedido);
        }

        public async Task<bool> PutPedido(PedidoViewModel pedidoViewModel)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            pedido.ProdutosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return await _pedidoService.PutPedido(pedido);
        }
    }
}
