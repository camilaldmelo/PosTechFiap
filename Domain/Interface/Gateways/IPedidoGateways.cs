using Domain.Entities;

namespace Domain.Interface.Gateways
{
    public interface IPedidoGateways
    {
        public Task<Pedido> GetById(int idPedido);
        public Task<IEnumerable<Pedido>> GetByIdStatus(int idAcompanhamento);
        public Task<IEnumerable<Pedido>> GetInProgress();
        public Task<IEnumerable<Pedido>> GetByIdCliente(int idCliente);
        public Task<IEnumerable<Pedido>> GetByIdProduto(int produtoId);
        public Task<int> Create(Pedido pedido);
        public Task<int> GetIdLastRecordInserted();
        public Task<bool> Update(Pedido pedido);
    }
}
    
