using Application.Interface;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        private readonly IPedidoUseCase _pedidoUseCase;

        public PedidoController(IPedidoUseCase pedidoUseCase)
        {
            _pedidoUseCase = pedidoUseCase;
        }

        /// <summary>
        /// Obtém um pedido por id
        /// </summary>
        /// <returns>Um pedido</returns>
        [HttpGet("{idPedido}", Name = "Pedidos")]
        [SwaggerOperation(Summary = "Obtém um pedido por id", Description = "Obtém um pedido por id do pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O pedido foi recuperado com sucesso.", typeof(CategoriaViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum pedido foi encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<PedidoViewModel> GetById(int idPedido)
        {
            return await _pedidoUseCase.GetById(idPedido);
        }

        /// <summary>
        /// Obtém uma lista de pedidos pelo status
        /// </summary>
        /// <returns>Uma lista de pedidos</returns>
        [HttpGet("Status/{idAcompanhamento}", Name = "PedidosPorStatus")]
        [SwaggerOperation(Summary = "Obtém uma lista de pedidos", Description = "Obtém uma lista de pedidos pelo id do status.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Os pedidos foram recuperados com sucesso.", typeof(IEnumerable<CategoriaViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum pedido foi encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IEnumerable<PedidoViewModel>> GetByIdStatus(int idAcompanhamento)
        {
            return await _pedidoUseCase.GetByIdStatus(idAcompanhamento);
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        /// <param name="pedido">Os dados do pedido a serem criados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o pedido não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPost(Name = "Pedido")]
        [SwaggerOperation(Summary = "Criação de um novo pedido", Description = "Cria um novo pedido.")]
        [SwaggerResponse(StatusCodes.Status201Created, "O pedido foi criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(PedidoIncViewModel pedido)
        {
            try
            {
                int idPedido = await _pedidoUseCase.Create(pedido);

                if (idPedido > 0)
                {
                    return Ok(); // Retorna 200 OK
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Atualiza o status de um pedido.
        /// </summary>
        /// <param name="idPedido">Id do pedido a ter seu status atualizão.</param>
        /// <param name="idStatus">Id do status que o pedido passará a ter.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o pedido não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut]
        [Route("PedidoStatus")]
        [SwaggerOperation(Summary = "Atualização do status de um pedido", Description = "Atualiza o status de um pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O status do pedido foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> PutStatus(int idPedido, int idStatus)
        {
            try
            {
                bool updated = await _pedidoUseCase.UpdateStatus(idPedido, idStatus);

                if (updated)
                {
                    return Ok(); // Retorna 200 OK
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Atualiza um pedido.
        /// </summary>
        /// <param name="pedido">Os dados do pedido a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o pedido não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut(Name = "Pedido")]
        [SwaggerOperation(Summary = "Atualização de um pedido", Description = "Atualiza um pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O pedido foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(PedidoIncViewModel pedido)
        {
            try
            {
                bool updated = await _pedidoUseCase.Update(pedido);

                if (updated)
                {
                    return Ok(); // Retorna 200 OK
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }
    }
}
