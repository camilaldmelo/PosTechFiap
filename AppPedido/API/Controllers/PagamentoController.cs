using Application.Interface.UseCases;
using Application.Presenters.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagamentoController : Controller
    {
        private readonly ILogger<PagamentoController> _logger;
        private readonly IPagamentoUseCase _pagamentoUseCase;

        public PagamentoController(ILogger<PagamentoController> logger, IPagamentoUseCase pagamentoUseCase)
        {
            _logger = logger;
            _pagamentoUseCase = pagamentoUseCase;
        }

        /// <summary>
        /// Realiza o pagamento do Pedido via o QRCode do Mercado Pago (Fake).
        /// </summary>
        [HttpPut("{idPedido}", Name = "PagamentoViaQRCodePeloIdDoPedido")]
        [SwaggerOperation(Summary = "Pagamento do Pedido via QRCode (Fake)", Description = "Realiza o pagamento do Pedido via QRCode (Fake).")]
        [SwaggerResponse(StatusCodes.Status200OK, "Requisição do processamento do pagamento do pedido, realizada com sucesso.", typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não foi encontrado o Pedido.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(int idPedido)
        {
            try
            {
                var requisitado = await _pagamentoUseCase.PagarViaQRCodeMercadoPago(idPedido);

                if (requisitado)
                {
                    return Ok(); // Retorna 200 OK se a atualização for bem-sucedida.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o acompanhamento não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Consulta o status do pagamento do Pedido (Fake).
        /// </summary>
        [HttpGet("{idPedido}", Name = "StatusPagamentoPorIdPedido")]
        [SwaggerOperation(Summary = "Consulta o status do pagamento do Pedido via QRCode (Fake)", Description = "Consulta o status do pagamento do Pedido via QRCode (Fake).")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o status do pagamento.", typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não foi encontrado o Pedido.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Get(int idPedido)
        {
            try
            {
                var pagamento = await _pagamentoUseCase.GetPagamentoByIdPedido(idPedido);
                var successResponse = new { status = "Aprovado" };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new { status = "Não Aprovado" };
                return NotFound(errorResponse);
            }
        }

        /// <summary>
        /// Finaliza o pagamento do pedido.
        /// </summary>
        /// <param name="pagamento">Dados do Pagamento.</param>
        /// <returns>
        /// 201 Created juntamente com o URL do novo recurso se a criação for bem-sucedida.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost(Name = "FinalizaPagamento")]
        [SwaggerOperation(Summary = "Finaliza o pagamento do pedido, caso o pagamento via QRCode seja Aprovado.", Description = "Finaliza o pagamento do pedido, caso o pagamento via QRCode seja Aprovado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o status do pagamento.", typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não foi encontrado o Pedido.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post([FromBody] PagamentoViewModel pagamento)
        {
            try
            {
                var idPagamento = await _pagamentoUseCase.Post(pagamento.IdPedido, pagamento.Aprovado, pagamento.Motivo);
                
                if (idPagamento == 0)
                {
                    return BadRequest($"Pagamento do pedido {pagamento.IdPedido} não foi aprovado. Motivo: {pagamento.Motivo}");
                }

                return CreatedAtRoute("StatusPagamentoPorIdPedido", new { idPedido = pagamento.IdPedido }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
