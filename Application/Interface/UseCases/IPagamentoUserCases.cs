﻿using Domain.Entities;


namespace Application.Interface.UserCases
{
    public interface IPagamentoUserCases
    {
        public Task<bool> PagarViaQRCodeMercadoPago(int idPedido);
        public Task<Pagamento> GetPagamentoByIdPedido(int idPedido);
        public Task<int> Post(int idPedido, bool aprovado, string? motivo);
    }
}