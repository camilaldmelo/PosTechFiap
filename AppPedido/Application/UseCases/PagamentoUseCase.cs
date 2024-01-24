using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Gateways;
using Application.Interface.UseCases;

namespace Application.UseCases
{
    public class PagamentoUseCase : IPagamentoUseCase
    {

        private readonly IPedidoUseCase _pedidoUseCase;
        private readonly iPagamentoGateway _pagamentoGateway;

        public PagamentoUseCase(IPedidoUseCase pedidoUseCase, iPagamentoGateway pagamentoGateway)
        {
            _pedidoUseCase = pedidoUseCase;
            _pagamentoGateway = pagamentoGateway;
        }

        public async Task<Pagamento> GetPagamentoByIdPedido(int idPedido)
        {
            var result = await _pagamentoGateway.GetByIdPedido(idPedido) ?? throw new Exception($"Pagamento não encontrado para o pedido [{idPedido}]");
            return result;
        }

        public async Task<bool> PagarViaQRCodeMercadoPago(int idPedido)
        {
            Pedido? pedido = await _pedidoUseCase.GetById(idPedido);

            if (pedido is null)
            {
                return false;
                throw new Exception($"Pedido {idPedido} não encontrado");
            }

            if (pedido.Acompanhamento?.Id != AcompanhamentoConst.Criado)
            {
                return false;
                throw new Exception($"Pedido {idPedido} não pode ser pago");
            }

            await Post(idPedido, true, "Pagamento aprovado com sucesso");
            return true;

            /*TODO: Criar alguma lógica para deixar random, a aprovação ou não do pagamento?*/
            //await Post(idPedido, false, "Pagamento reprovad");
        }

        public async Task<int> Post(int idPedido, bool aprovado, string? motivo)
        {
            if (!aprovado)
            {
                return 0;
                throw new Exception($"Pagamento não aprovado para o pedido [{idPedido}] pelo motivo [{motivo}]");
            }
            else
            {
                var pedido = await _pedidoUseCase.GetById(idPedido) ?? throw new Exception($"Pedido não encontrado [{idPedido}]");
                Pagamento pagamento = pedido.GerarPagamento(MetodoPagamentoConst.MercadoPagoQRCode);
                pagamento.Id = await _pagamentoGateway.Create(pagamento);
                await _pedidoUseCase.UpdateStatus(idPedido, AcompanhamentoConst.Recebido);
                return pagamento.Id;
            }
        }
    }
}
