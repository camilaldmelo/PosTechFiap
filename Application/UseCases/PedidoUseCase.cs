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

        public async Task<IEnumerable<PedidoViewModel>> GetById(int idPedido)
        {
            var pedido = await _pedidoService.GetById(idPedido);

            return _mapper.Map<List<PedidoViewModel>>(pedido);
        }

        public async Task<int> Create(PedidoViewModel pedidoViewModel)
        {
            var cliente = _mapper.Map<Cliente>(pedidoViewModel);
            var produtosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return await _pedidoService.Create(cliente, produtosPedido);
        }

        public async Task<bool> Update(PedidoViewModel pedidoViewModel)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            pedido.ProdutosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return await _pedidoService.Update(pedido);
        }
    }
}
