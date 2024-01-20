using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteUseCase _clienteUseCase;

        public ClienteController(ILogger<ClienteController> logger, IClienteUseCase clienteUseCase)
        {
            _logger = logger;
            _clienteUseCase = clienteUseCase;
        }

        /// <summary>
        /// Obtém uma lista de todos os clientes.
        /// </summary>
        /// <returns>
        /// 200 OK com a lista de clientes recuperada com sucesso.
        /// 404 Not Found se nenhum cliente for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet(Name = "Clientes")]
        [SwaggerOperation(Summary = "Listagem de todos os clientes", Description = "Recupera uma lista de todos os clientes.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de clientes foi recuperada com sucesso.", typeof(IEnumerable<ClienteViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum cliente encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clientes = await _clienteUseCase.GetAll();

                if (clientes != null && clientes.Any())
                {
                    return Ok(clientes); // Retorna 200 OK com os clientes recuperados.
                }
                else if (clientes != null)
                {
                    return NotFound(); // Retorna 404 Not Found se não houver clientes encontrados.
                }
                else
                {
                    return StatusCode(500, "Erro interno do servidor"); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Obtém um cliente com base no identificador.
        /// </summary>
        /// <param name="id">O identificador do cliente desejado.</param>
        /// <returns>
        /// 200 OK com o cliente recuperado.
        /// 404 Not Found se o cliente não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet("{id}", Name = "ClientePorId")]
        [SwaggerOperation(Summary = "Obtenção de cliente por ID", Description = "Obtém um cliente com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O cliente foi recuperado com sucesso.", typeof(ClienteViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Cliente não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cliente = await _clienteUseCase.GetById(id);

                if (cliente != null)
                {
                    return Ok(cliente); // Retorna 200 OK com o cliente recuperado.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o cliente não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Obtém um cliente com base no CPF especificado.
        /// </summary>
        /// <param name="cpf">O CPF do cliente desejado.</param>
        /// <returns>
        /// 200 OK com o cliente recuperado.
        /// 404 Not Found se o cliente não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet("cpf/{cpf}", Name = "ClientePorCPF")]
        [SwaggerOperation(Summary = "Obtenção de cliente por CPF", Description = "Obtém um cliente com base no CPF especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O cliente foi recuperado com sucesso.", typeof(ClienteViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Cliente não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetByCPF(string cpf)
        {
            try
            {
                var cliente = await _clienteUseCase.GetByCPF(cpf);

                if (cliente != null)
                {
                    return Ok(cliente); // Retorna 200 OK com o cliente recuperado.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o cliente não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="cliente">Os dados do novo cliente a ser criado.</param>
        /// <returns>
        /// 201 Created juntamente com o URL do novo recurso se a criação for bem-sucedida.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost(Name = "Clientes")]
        [SwaggerOperation(Summary = "Criação de um novo cliente", Description = "Cria um novo cliente com base nos dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "O cliente foi criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(ClienteViewModel cliente)
        {
            try
            {
                int clienteId = await _clienteUseCase.Create(cliente);
                return CreatedAtRoute("ClientePorId", new { id = clienteId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um cliente com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="cliente">Os dados do cliente a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o cliente não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut("{id}", Name = "ClientePorId")]
        [SwaggerOperation(Summary = "Atualização de um cliente por ID", Description = "Atualiza um cliente com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O cliente foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Cliente não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(int id, ClienteViewModel cliente)
        {
            try
            {
                bool updated = await _clienteUseCase.Update(id, cliente);

                if (updated)
                {
                    return Ok(); // Retorna 200 OK se a atualização for bem-sucedida.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o cliente não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Exclui um cliente com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser excluído.</param>
        /// <returns>
        /// 204 No Content se o cliente for excluído com sucesso.
        /// 404 Not Found se o cliente não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpDelete("{id}", Name = "ClientePorId")]
        [SwaggerOperation(Summary = "Exclusão de cliente por ID", Description = "Exclui um cliente com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "O cliente foi excluído com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Cliente não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _clienteUseCase.Delete(id);

                if (result)
                {
                    return NoContent(); // Retorna 204 No Content se o cliente for excluído com sucesso.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o cliente não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }
    }
}
