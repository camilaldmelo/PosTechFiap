using Application.Interface.Presenters;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using Domain.UseCases;

namespace Application.Presenters
{
    public class PagamentoUseCase : IPagamentoUseCase
    {
        private readonly IPagamentoService _pagamentoService;
        public PagamentoUseCase(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        public async Task<Pagamento> GetPagamentoByIdPedido(int idPedido)
        {
            try
            {
                return await _pagamentoService.GetPagamentoByIdPedido(idPedido);
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
                return await _pagamentoService.PagarViaQRCodeMercadoPago(idPedido);
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
                return await _pagamentoService.Post(idPedido, aprovado, motivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
