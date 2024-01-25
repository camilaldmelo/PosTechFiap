using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoUseCase _pagamentoUseCase;
        public PagamentoController(IPagamentoUseCase pagamentoUseCase)
        {
            _pagamentoUseCase = pagamentoUseCase;
        }

        /// <summary>
        /// Cria um QRCode para pagamento do Pedido em questão.
        /// </summary>
        /// <param name="pagamento">Dados do Pedido que será gerado o QRCode para Pagamento.</param>
        /// <returns>
        /// 200 Retorna a string do QRCode.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost("GeraQRCodeParaPagamento")]
        public async Task<IActionResult> GeraQRCodeParaPagamento([FromBody] Pagamento pagamento)
        {
            var qrCodeUrl = await _pagamentoUseCase.GeraQRCodeParaPagamento(pagamento);
            return Ok(qrCodeUrl);
        }

        /// <summary>
        /// Realiza o pagamento do QRCode.
        /// Esse metodo também irá notificar o WebHook de Pedidos que o pagamento foi realizado.
        /// </summary>
        /// <param name="qrCode">QRCode que será pago.</param>
        /// <returns>
        /// 200 OK Pagamento realizado.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost("PagaQRCode")]
        public async Task<IActionResult> PagaQRCode([FromBody] string qrCode)
        {
            await _pagamentoUseCase.PagaQRCode(qrCode);
            return Ok();
        }
    }
}
