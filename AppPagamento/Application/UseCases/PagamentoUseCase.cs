using Application.Interfaces;
using Domain.Entities;
using Domain.Interface.Services;
using Newtonsoft.Json;
using System.Text;

namespace Application.UseCases
{
    public class PagamentoUseCase : IPagamentoUseCase
    {
        private readonly IPedidoWebhookSender _pedidoWebhookSender;
        public PagamentoUseCase(IPedidoWebhookSender pedidoWebhookSender) 
        {
            _pedidoWebhookSender = pedidoWebhookSender;
        }

        public async Task<string> GeraQRCodeParaPagamento(Pagamento pagamento)
        {
            string jsonString = JsonConvert.SerializeObject(pagamento);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }

        public async Task PagaQRCode(string QRCode)
        {
            byte[] bytes = Convert.FromBase64String(QRCode);
            string jsonString = Encoding.UTF8.GetString(bytes);
            Pagamento pagamento = JsonConvert.DeserializeObject<Pagamento>(jsonString);

            await _pedidoWebhookSender.NotificaPagamentoAoPedido(pagamento);
        }
    }
}
