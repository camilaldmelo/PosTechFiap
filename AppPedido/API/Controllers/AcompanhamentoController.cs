using Application.Interface.Presenters;
using Application.Interface.UseCases;
using Application.Presenters.ViewModel;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcompanhamentoController : Controller
    {
        private readonly ILogger<AcompanhamentoController> _logger;
        private readonly IAcompanhamentoPresenter _acompanhamentoPresenter;
        private readonly IAcompanhamentoUseCase _acompanhamentoUseCase;

        public AcompanhamentoController(ILogger<AcompanhamentoController> logger, IAcompanhamentoPresenter acompanhamentoPresenter, IAcompanhamentoUseCase acompanhamentoUseCase)
        {
            _logger = logger;
            _acompanhamentoPresenter = acompanhamentoPresenter;
            _acompanhamentoUseCase = acompanhamentoUseCase;
        }

        /// <summary>
        /// Obtém uma lista de todos os acompanhamentos.
        /// </summary>
        /// <returns>Uma lista de acompanhamentos.</returns>
        [HttpGet(Name = "Acompanhamentos")]
        [SwaggerOperation(Summary = "Listagem de todos os acompanhamentos", Description = "Recupera uma lista de todos os acompanhamentos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de acompanhamentos foi recuperada com sucesso.", typeof(IEnumerable<AcompanhamentoViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum acompanhamento encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var acompanhamentos = await _acompanhamentoUseCase.GetAll();
                var acompanhamentosView = await _acompanhamentoPresenter.ConvertToListViewModel(acompanhamentos);

                if (acompanhamentosView != null && acompanhamentosView.Any())
                {
                    return Ok(acompanhamentosView); // Retorna 200 OK com os acompanhamentos recuperados.
                }
                else if (acompanhamentosView != null)
                {
                    return NotFound(); // Retorna 404 Not Found se não houver acompanhamentos encontrados.
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
        /// Obtém um acompanhamento com base no identificador.
        /// </summary>
        /// <param name="id">O identificador do acompanhamento desejado.</param>
        /// <returns>
        /// 200 OK com o acompanhamento recuperado.
        /// 404 Not Found se o acompanhamento não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet("{id}", Name = "AcompanhamentoPorId")]
        [SwaggerOperation(Summary = "Obtenção de acompanhamento por ID", Description = "Obtém um acompanhamento com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O acompanhamento foi recuperado com sucesso.", typeof(AcompanhamentoViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Acompanhamento não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var acompanhamento = await _acompanhamentoUseCase.GetById(id);
                var acompanhamentosView = await _acompanhamentoPresenter.ConvertToViewModel(acompanhamento);

                if (acompanhamentosView != null)
                {
                    return Ok(acompanhamentosView); // Retorna 200 OK com o acompanhamento recuperado.
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
        /// Cria um novo acompanhamento.
        /// </summary>
        /// <param name="acompanhamento">Os dados do novo acompanhamento a ser criado.</param>
        /// <returns>
        /// 201 Created juntamente com o URL do novo recurso se a criação for bem-sucedida.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost(Name = "Acompanhamentos")]
        [SwaggerOperation(Summary = "Criação de um novo acompanhamento", Description = "Cria um novo acompanhamento com base nos dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "O acompanhamento foi criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(AcompanhamentoViewModel acompanhamento)
        {
            try
            {
                var acompanhamentoModel = await _acompanhamentoPresenter.ConvertFromViewModel(acompanhamento);
                int acompanhamentoId = await _acompanhamentoUseCase.Create(acompanhamentoModel);
                return CreatedAtRoute("AcompanhamentoPorId", new { id = acompanhamentoId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        /// <summary>
        /// Atualiza um acompanhamento com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do acompanhamento a ser atualizado.</param>
        /// <param name="acompanhamento">Os dados do acompanhamento a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o acompanhamento não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut("{id}", Name = "AcompanhamentoPorId")]
        [SwaggerOperation(Summary = "Atualização de um acompanhamento por ID", Description = "Atualiza um acompanhamento com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O acompanhamento foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Acompanhamento não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(int id, AcompanhamentoViewModel acompanhamento)
        {
            try
            {
                var acompanhamentoModel = await _acompanhamentoPresenter.ConvertFromViewModel(acompanhamento);
                bool updated = await _acompanhamentoUseCase.Update(acompanhamentoModel, id);

                if (updated)
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
        /// Exclui um acompanhamento com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do acompanhamento a ser excluído.</param>
        /// <returns>
        /// 204 No Content se o acompanhamento for excluído com sucesso.
        /// 404 Not Found se o acompanhamento não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpDelete("{id}", Name = "AcompanhamentoPorId")]
        [SwaggerOperation(Summary = "Exclusão de acompanhamento por ID", Description = "Exclui um acompanhamento com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "O acompanhamento foi excluído com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Acompanhamento não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _acompanhamentoUseCase.Delete(id);

                if (result)
                {
                    return NoContent(); // Retorna 204 No Content se o acompanhamento for excluído com sucesso.
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
    }
}
