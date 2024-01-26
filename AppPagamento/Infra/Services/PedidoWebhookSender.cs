using Domain.Entities;
using Domain.Interface.Services;
using Newtonsoft.Json;
using System.Text;

namespace Infra.Services
{
    public class PedidoWebhookSender : IPedidoWebhookSender
    {
        private readonly string webhookUrl;

        public PedidoWebhookSender()
        {
            webhookUrl = Environment.GetEnvironmentVariable("WEBHOOK_URL") ?? "http://localhost:8080/Pagamento";
        }
        public async Task NotificaPagamentoAoPedido(Pagamento pagamento)
        {
            var payload = new WebhookNotificacao { IdPedido = pagamento.IdPedido, Aprovado = true, Motivo = "Pagamento aprovado com sucesso!" };

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                await httpClient.PostAsync(webhookUrl, content);
            }
        }
    }
}
