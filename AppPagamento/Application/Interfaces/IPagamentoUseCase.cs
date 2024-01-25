using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagamentoUseCase
    {
        Task<string> GeraQRCodeParaPagamento(Pagamento pagamento);

        Task PagaQRCode(string QRCode);
    }
}
