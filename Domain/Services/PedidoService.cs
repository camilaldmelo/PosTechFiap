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
        public IUnitOfWork _unitOfWork;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutosPedidoRepository produtosPedidoRepository, IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _produtosPedidoRepository = produtosPedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Pedido>> GetPedido(int idPedido)
        {
            var pedidos = await _pedidoRepository.ObterPedidosPorId(idPedido);

            foreach(var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoRepository.ObterProdutoPedidoPorPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<int> PostPedido(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido)
        {
            var idPedido = await _pedidoRepository.ObterIdUltimoRegistroInserido() + 1;
            var pedido = new Pedido(id:idPedido, idCliente:cliente.Id, data:DateTime.Now, idAcompanhamento:AcompanhamentoConst.Recebido, null);

            try
            {
                _unitOfWork.BeginTransaction();

                await _pedidoRepository.InserirPedido(pedido);
                foreach (var pp in produtosPedido)
                {
                    pp.IdPedido = idPedido;
                    await _produtosPedidoRepository.InserirProdutoPedido(pp);
                }

                _unitOfWork.Commit();                
            }
            catch
            {
                _unitOfWork.Rollback();
            }
            return idPedido;
        }

        public async Task<bool> PutPedido(Pedido pedido)
        {
            try
            {
                _unitOfWork.BeginTransaction();                
                await _pedidoRepository.AtualizarPedido(pedido);
                await _produtosPedidoRepository.DeletarProdutoPedidoPorIdPedido(pedido.Id);

                if (pedido.ProdutosPedido != null)
                {
                    foreach (var pp in pedido.ProdutosPedido)
                    {
                        pp.IdPedido = pedido.Id;
                        await _produtosPedidoRepository.InserirProdutoPedido(pp);
                    }
                }
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
            }
            return true;
        }
    }
}
