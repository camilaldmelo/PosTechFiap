using Application.Interface.Presenters;
using Application.Presenters;
using Application.Presenters.ViewModel;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagamentoController : Controller
    {
        private readonly ILogger<PagamentoController> _logger;
        private readonly IPagamentoPresenter _pagamentoPresenter;

        public PagamentoController(ILogger<PagamentoController> logger, IPagamentoPresenter pagamentoPresenter)
        {
            _logger = logger;
            _pagamentoPresenter = pagamentoPresenter;
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
                var pagamento = await _pagamentoPresenter.GetPagamentoByIdPedido(idPedido);
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
        /// Webhook para realizar o pagamento do Pedido.
        /// Será chamado pelo serviço de pagamento fake.
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
                var idPagamento = await _pagamentoPresenter.Post(pagamento.IdPedido, pagamento.Aprovado, pagamento.Motivo);
                
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
