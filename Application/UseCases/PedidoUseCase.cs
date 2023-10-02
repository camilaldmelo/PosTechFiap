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

        public IEnumerable<PedidoViewModel> GetPedido(int idAcompanhamento)
        {
            var pedido = _pedidoService.GetPedido(idAcompanhamento);

            return _mapper.Map<List<PedidoViewModel>>(pedido);
        }

        public int PostPedido(PedidoViewModel pedidoViewModel)
        {            
            var cliente = _mapper.Map<Cliente>(pedidoViewModel);
            var produtosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return _pedidoService.PostPedido(cliente, produtosPedido);
        }

        public bool PutPedido(PedidoViewModel pedidoViewModel)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            pedido.ProdutosPedido = _mapper.Map<List<ProdutosPedido>>(pedidoViewModel.ProdutosPedido);

            return _pedidoService.PutPedido(pedido);
        }
    }
}
