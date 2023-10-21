using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IPedidoRepository
    {
        public Task<IEnumerable<Pedido>> GetById(int idPedido);

        public Task<bool> Create(Pedido pedido);

        public Task<int> GetIdLastRecordInserted();

        public Task<bool> Update(Pedido pedido);
    }
}
