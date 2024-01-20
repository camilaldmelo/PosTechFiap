using Domain.Entities;


namespace Application.Interface.UseCases
{
    public interface IPagamentoUseCases
    {
        public Task<bool> PagarViaQRCodeMercadoPago(int idPedido);
        public Task<Pagamento> GetPagamentoByIdPedido(int idPedido);
        public Task<int> Post(int idPedido, bool aprovado, string? motivo);
    }
}
