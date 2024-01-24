using Application.Interface.UseCases;
using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Gateways;

namespace Application.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IProdutosPedidoGateway _produtosPedidoGateway;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoUseCase(IPedidoGateway pedidoGateway, IProdutosPedidoGateway produtosPedidoGateway, IUnitOfWork unitOfWork)
        {
            _pedidoGateway = pedidoGateway;
            _produtosPedidoGateway = produtosPedidoGateway;
            _unitOfWork = unitOfWork;
        }

        public async Task<Pedido> GetById(int idPedido)
        {
            var pedido = await _pedidoGateway.GetById(idPedido);
            pedido.ProdutosPedido = await _produtosPedidoGateway.GetByIdPedido(pedido.Id);
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> GetByIdStatus(int idAcompanhamento)
        {
            var pedidos = await _pedidoGateway.GetByIdStatus(idAcompanhamento);

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateway.GetByIdPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetInProgress()
        {
            var pedidos = await _pedidoGateway.GetInProgress();

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateway.GetByIdPedido(pedido.Id);
            }

            return pedidos.OrderByDescending(pedido => pedido.IdAcompanhamento)
                          .ThenBy(pedido => pedido.Data);
        }

        public async Task<IEnumerable<Pedido>> GetByIdCliente(int idCliente)
        {
            var pedidos = await _pedidoGateway.GetByIdCliente(idCliente);

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateway.GetByIdPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByIdProduto(int idProduto)
        {
            var pedidos = await _pedidoGateway.GetByIdProduto(idProduto);

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoGateway.GetByIdPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<int> Create(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido)
        {
            var pedido = new Pedido(idCliente:cliente.Id, data:DateTime.Now, idAcompanhamento:AcompanhamentoConst.Criado);
            try
            {
                _unitOfWork.BeginTransaction();

                pedido.Id = await _pedidoGateway.Create(pedido);
                foreach (var pp in produtosPedido)
                {
                    pp.IdPedido = pedido.Id;
                    await _produtosPedidoGateway.Create(pp);
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
                var pedido = await _pedidoGateway.GetById(idPedido) ?? throw new Exception("O pedido fornecido não existe.");
                pedido.IdAcompanhamento = idStatus;

                _unitOfWork.BeginTransaction();
                await _pedidoGateway.Update(pedido);
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
                var pedidoUpdate = await _pedidoGateway.GetById(pedido.Id) ?? throw new Exception("O pedido fornecido não existe.");
                pedido.IdAcompanhamento = pedidoUpdate.IdAcompanhamento;

                _unitOfWork.BeginTransaction();                
                await _pedidoGateway.Update(pedido);
                await _produtosPedidoGateway.DeleteByIdPedido(pedido.Id);

                if (pedido.ProdutosPedido != null)
                {
                    foreach (var pp in pedido.ProdutosPedido)
                    {
                        pp.IdPedido = pedido.Id;
                        await _produtosPedidoGateway.Create(pp);
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
