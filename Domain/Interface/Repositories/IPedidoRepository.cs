using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IPedidoRepository
    {
        public Task<Pedido> GetById(int idPedido);

        public Task<IEnumerable<Pedido>> GetByIdStatus(int idAcompanhamento);

        public Task<int> Create(Pedido pedido);

        public Task<int> GetIdLastRecordInserted();

        public Task<bool> Update(Pedido pedido);
    }
}
