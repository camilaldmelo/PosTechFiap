using Domain.DTO;
using Domain.Entities;

namespace Application.UseCases
{
    public class PedidoUseCases : IPedidoUseCases
    {
        private readonly IPedidoGateways _pedidoGateways;
        private readonly IProdutosPedidoGateways _produtosPedidoGateways;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoUseCases(IPedidoGateways pedidoGateways, IProdutosPedidoGateways produtosPedidoGateways, IUnitOfWork unitOfWork)
        {
            _pedidoGateways = pedidoGateways;
            _produtosPedidoGateways = produtosPedidoGateways;
            _unitOfWork = unitOfWork;
        }

        public async Task<Pedido> GetById(int idPedido)
        {
            var pedido = await _pedidoGateways.GetById(idPedido);
            pedido.ProdutosPedido = await _produtosPedidoGateways.GetByIdPedido(pedido.Id);
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> GetByIdStatus(int idAcompanhamento)
        {
            var pedidos = await _pedidoGateways.GetByIdStatus(idAcompanhamento);

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateways.GetByIdPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetInProgress()
        {
            var pedidos = await _pedidoGateways.GetInProgress();

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateways.GetByIdPedido(pedido.Id);
            }

            return pedidos.OrderByDescending(pedido => pedido.IdAcompanhamento)
                          .ThenBy(pedido => pedido.Data);
        }

        public async Task<IEnumerable<Pedido>> GetByIdCliente(int idCliente)
        {
            var pedidos = await _pedidoGateways.GetByIdCliente(idCliente);

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateways.GetByIdPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByIdProduto(int idProduto)
        {
            var pedidos = await _pedidoGateways.GetByIdProduto(idProduto);

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateways.GetByIdPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<int> Create(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido)
        {
            var pedido = new Pedido(idCliente:cliente.Id, data:DateTime.Now, idAcompanhamento:AcompanhamentoConst.Criado);
            try
            {
                _unitOfWork.BeginTransaction();

                pedido.Id = await _pedidoGateways.Create(pedido);
                foreach (var pp in produtosPedido)
                {
                    pp.IdPedido = pedido.Id;
                    await _produtosPedidoGateways.Create(pp);
                }

                _unitOfWork.Commit();                
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            return pedido.Id;
        }

        public async Task<bool> UpdateStatus(int idPedido, int idStatus)
        {
            try
            {
                var pedido = await _pedidoGateways.GetById(idPedido) ?? throw new Exception("O pedido fornecido não existe.");
                pedido.IdAcompanhamento = idStatus;

                _unitOfWork.BeginTransaction();
                await _pedidoGateways.Update(pedido);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            return true;
        }

        public async Task<bool> Update(Pedido pedido)
        {
            try
            {
                var pedidoUpdate = await _pedidoGateways.GetById(pedido.Id) ?? throw new Exception("O pedido fornecido não existe.");
                pedido.IdAcompanhamento = pedidoUpdate.IdAcompanhamento;

                _unitOfWork.BeginTransaction();                
                await _pedidoGateways.Update(pedido);
                await _produtosPedidoGateways.DeleteByIdPedido(pedido.Id);

                if (pedido.ProdutosPedido != null)
                {
                    foreach (var pp in pedido.ProdutosPedido)
                    {
                        pp.IdPedido = pedido.Id;
                        await _produtosPedidoGateways.Create(pp);
                    }
                }
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}
