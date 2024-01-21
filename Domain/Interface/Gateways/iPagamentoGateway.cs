using Domain.Entities;

namespace Domain.Interface.Gateways
{
    public interface iPagamentoGateway
    {
        public Task<Pagamento?> GetByIdPedido(int idPedido);
        public Task<int> Create(Pagamento pagamento);
    }
}
