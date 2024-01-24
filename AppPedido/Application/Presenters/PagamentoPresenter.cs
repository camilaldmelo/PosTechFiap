using Application.Interface.Presenters;
using Application.Interface.UseCases;
using Domain.Entities;

namespace Application.Presenters
{
    public class PagamentoPresenter : IPagamentoPresenter
    {
        private readonly IPagamentoUseCase _pagamentoUseCase;
        public PagamentoPresenter(IPagamentoUseCase pagamentoUseCase)
        {
            _pagamentoUseCase = pagamentoUseCase;
        }

        public async Task<Pagamento> GetPagamentoByIdPedido(int idPedido)
        {
            try
            {
                return await _pagamentoUseCase.GetPagamentoByIdPedido(idPedido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> PagarViaQRCodeMercadoPago(int idPedido)
        {
            try
            {
                return await _pagamentoUseCase.PagarViaQRCodeMercadoPago(idPedido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Post(int idPedido, bool aprovado, string? motivo)
        {
            try
            {
                return await _pagamentoUseCase.Post(idPedido, aprovado, motivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
