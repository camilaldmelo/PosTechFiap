using Domain.Entities;


namespace Domain.Interface.Services
{
    public interface IPagamentoService
    {
        public Task<bool> PagarViaQRCodeMercadoPago(int idPedido);
        public Task<Pagamento> GetPagamentoByIdPedido(int idPedido);
        public Task<int> Post(int idPedido, bool aprovado, string? motivo);
    }
}
