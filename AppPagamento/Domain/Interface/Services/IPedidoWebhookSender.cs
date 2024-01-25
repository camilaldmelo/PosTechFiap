using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface IPedidoWebhookSender
    {
        Task NotificaPagamentoAoPedido(Pagamento pagamento);
    }
}
