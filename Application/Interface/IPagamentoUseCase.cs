using Application.ViewModel;
using Domain.Entities;

namespace Application.Interface
{
    public interface IPagamentoUseCase
    {
        public Task<bool> PagarViaQRCodeMercadoPago(int idPedido);
        public Task<Pagamento> GetPagamentoByIdPedido(int idPedido);
        public Task<int> Post(int idPedido, bool aprovado, string? motivo);
    }
}
