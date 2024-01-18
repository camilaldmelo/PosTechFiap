using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IPagamentoRepository
    {
        public Task<Pagamento?> GetByIdPedido(int idPedido);
        public Task<int> Create(Pagamento pagamento);
    }
}
