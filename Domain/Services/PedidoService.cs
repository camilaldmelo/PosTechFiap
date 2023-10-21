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

        public async Task<IEnumerable<Pedido>> GetById(int idPedido)
        {
            var requests = await _pedidoRepository.GetById(idPedido);

            foreach(var request in requests)
            {
                request.ProdutosPedido = await _produtosPedidoRepository.GetByIdPedido(request.Id);
            }

            return requests;
        }

        public async Task<int> Create(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido)
        {
            var idPedido = await _pedidoRepository.GetIdLastRecordInserted() + 1;
            var pedido = new Pedido(id:idPedido, idCliente:cliente.Id, data:DateTime.Now, idAcompanhamento:AcompanhamentoConst.Recebido, null);

            try
            {
                _unitOfWork.BeginTransaction();

                await _pedidoRepository.Create(pedido);
                foreach (var pp in produtosPedido)
                {
                    pp.IdPedido = idPedido;
                    await _produtosPedidoRepository.Create(pp);
                }

                _unitOfWork.Commit();                
            }
            catch
            {
                _unitOfWork.Rollback();
            }
            return idPedido;
        }

        public async Task<bool> Update(Pedido pedido)
        {
            try
            {
                _unitOfWork.BeginTransaction();                
                await _pedidoRepository.Update(pedido);
                await _produtosPedidoRepository.DeleteByIdPedido(pedido.Id);

                if (pedido.ProdutosPedido != null)
                {
                    foreach (var pp in pedido.ProdutosPedido)
                    {
                        pp.IdPedido = pedido.Id;
                        await _produtosPedidoRepository.Create(pp);
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
