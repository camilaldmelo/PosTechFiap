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
        [HttpGet("{id}", Name = "PedidoPorId")]
        [SwaggerOperation(Summary = "Obtém um pedido por id", Description = "Obtém um pedido por id do pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O pedido foi recuperado com sucesso.", typeof(IEnumerable<CategoriaViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum pedido foi encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IEnumerable<PedidoViewModel>> GetById(int idPedido)
        {
            return await _pedidoUseCase.GetById(idPedido);
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        /// <param name="pedido">Os dados do pedido a serem criados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se a categoria não for encontrada com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPost(Name = "Pedido")]
        [SwaggerOperation(Summary = "Criação de um novo pedido", Description = "Cria um novo pedido.")]
        [SwaggerResponse(StatusCodes.Status201Created, "O pedido foi criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(PedidoViewModel pedido)
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
        /// Atualiza um pedido.
        /// </summary>
        /// <param name="pedido">Os dados do pedido a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se a categoria não for encontrada com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut(Name = "Pedido")]
        [SwaggerOperation(Summary = "Atualização de uma pedido", Description = "Atualiza um pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O pedido foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(PedidoViewModel pedido)
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
