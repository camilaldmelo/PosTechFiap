using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaPresenter _categoriaPresenter;

        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaPresenter categoriaPresenter)
        {
            _logger = logger;
            _categoriaPresenter = categoriaPresenter;
        }

        /// <summary>
        /// Obtém uma lista de todas as categorias.
        /// </summary>
        /// <returns>Uma lista de categorias.</returns>
        [HttpGet(Name = "Categorias")]
        [SwaggerOperation(Summary = "Listagem de todas as categorias", Description = "Recupera uma lista de todas as categorias.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de categorias foi recuperada com sucesso.", typeof(IEnumerable<CategoriaViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhuma categoria encontrada.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categorias = await _categoriaPresenter.GetAll();

                if (categorias != null && categorias.Any())
                {
                    return Ok(categorias); // Retorna 200 OK com as categorias recuperadas.
                }
                else if (categorias != null)
                {
                    return NotFound(); // Retorna 404 Not Found se não houver categorias encontradas.
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
        /// Obtém uma categoria com base no identificador.
        /// </summary>
        /// <param name="id">O identificador da categoria desejada.</param>
        /// <returns>
        /// 200 OK com a categoria recuperada.
        /// 404 Not Found se a categoria não for encontrada.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet("{id}", Name = "CategoriaPorId")]
        [SwaggerOperation(Summary = "Obtenção de categoria por ID", Description = "Obtém uma categoria com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A categoria foi recuperada com sucesso.", typeof(CategoriaViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria não encontrada.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var categoria = await _categoriaPresenter.GetById(id);

                if (categoria != null)
                {
                    return Ok(categoria); // Retorna 200 OK com a categoria recuperada.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se a categoria não for encontrada.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoria">Os dados da nova categoria a ser criada.</param>
        /// <returns>
        /// 201 Created juntamente com o URL do novo recurso se a criação for bem-sucedida.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost(Name = "Categorias")]
        [SwaggerOperation(Summary = "Criação de uma nova categoria", Description = "Cria uma nova categoria com base nos dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "A categoria foi criada com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(CategoriaViewModel categoria)
        {
            try
            {
                int categoriaId = await _categoriaPresenter.Create(categoria);
                return CreatedAtRoute("CategoriaPorId", new { id = categoriaId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma categoria com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID da categoria a ser atualizada.</param>
        /// <param name="categoria">Os dados da categoria a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se a categoria não for encontrada com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut("{id}", Name = "CategoriaPorId")]
        [SwaggerOperation(Summary = "Atualização de uma categoria por ID", Description = "Atualiza uma categoria com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A categoria foi atualizada com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria não encontrada.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(int id, CategoriaViewModel categoria)
        {
            try
            {
                bool updated = await _categoriaPresenter.Update(id, categoria);

                if (updated)
                {
                    return Ok(); // Retorna 200 OK se a atualização for bem-sucedida.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se a categoria não for encontrada.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Exclui uma categoria com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID da categoria a ser excluída.</param>
        /// <returns>
        /// 204 No Content se a categoria for excluída com sucesso.
        /// 404 Not Found se a categoria não for encontrada.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpDelete("{id}", Name = "CategoriaPorId")]
        [SwaggerOperation(Summary = "Exclusão de categoria por ID", Description = "Exclui uma categoria com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "A categoria foi excluída com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria não encontrada.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _categoriaPresenter.Delete(id);

                if (result)
                {
                    return NoContent(); // Retorna 204 No Content se a categoria for excluída com sucesso.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se a categoria não for encontrada.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }
    }
}
