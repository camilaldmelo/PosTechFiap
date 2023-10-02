using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class PedidoService : IPedidoService
    {
        public IPedidoRepository _pedidoRepository;
        public IProdutosPedidoRepository _produtosPedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutosPedidoRepository produtosPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtosPedidoRepository = produtosPedidoRepository;
        }

        public IEnumerable<Pedido> GetPedido(int idAcompanhamento)
        {
            var pedidos = _pedidoRepository.ObterPedidosPorStatusAcompanhamento(idAcompanhamento);

            foreach(var pedido in pedidos)
            {
                pedido.ProdutosPedido = _produtosPedidoRepository.ObterProdutoPedidoPorPedido(pedido.Id);
            }

            return pedidos;
        }

        public int PostPedido(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido)
        {
            var pedido = new Pedido(idCliente:cliente.Id, data:DateTime.Now, idAcompanhamento:AcompanhamentoConst.Recebido, null);
            var idPedido = _pedidoRepository.InserirPedido(pedido);

            foreach(var pp in produtosPedido)
            {
                pp.IdPedido = idPedido;
                _produtosPedidoRepository.InserirProdutoPedido(pp);
            }
            
            return idPedido;
        }

        public bool PutPedido(Pedido pedido)
        {
            _pedidoRepository.AtualizarPedido(pedido);
            _produtosPedidoRepository.DeletarProdutoPedidoPorIdPedido(pedido.Id);

            if (pedido.ProdutosPedido != null)
            {
                foreach (var pp in pedido.ProdutosPedido)
                {
                    pp.IdPedido = pedido.Id;
                    _produtosPedidoRepository.InserirProdutoPedido(pp);
                }
            }

            return true;
        }
    }
}
