using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : Controller
    {

        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoPresenter _produtoPresenter;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoPresenter produtoPresenter)
        {
            _logger = logger;
            _produtoPresenter = produtoPresenter;
        }

        /// <summary>
        /// Obtém uma lista de todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        [HttpGet(Name = "Produtos")]
        [SwaggerOperation(Summary = "Listagem de todos os produtos", Description = "Recupera uma lista de todos os produtos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de produtos foi recuperada com sucesso.", typeof(IEnumerable<ProdutoViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum produto encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var produtos = await _produtoPresenter.GetAll();

                if (produtos != null && produtos.Any())
                {
                    return Ok(produtos); // Retorna 200 OK com os produtos recuperados.
                }
                else if (produtos != null)
                {
                    return NotFound(); // Retorna 404 Not Found se não houver produtos encontrados.
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
        /// Obtém uma lista de produtos com base no identificador da categoria.
        /// </summary>
        /// <param name="idCategoria">O identificador da categoria dos produtos desejada.</param>
        /// <returns>Uma lista de produtos filtrados pela categoria especificada.</returns>
        [HttpGet("Categoria/{idCategoria}", Name = "ProdutosPorCategoria")]
        [SwaggerOperation(Summary = "Listagem de produtos por categoria", Description = "Recupera uma lista de produtos filtrados pela categoria especificada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de produtos foi recuperada com sucesso.", typeof(IEnumerable<ProdutoViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum produto encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetByIdCategoria(int idCategoria)
        {
            try
            {
                var produtos = await _produtoPresenter.GetByIdCategoria(idCategoria);

                if (produtos != null && produtos.Any())
                {
                    return Ok(produtos); // Retorna 200 OK com os produtos recuperados.
                }
                else if (produtos != null)
                {
                    return NotFound(); // Retorna 404 Not Found se não houver produtos encontrados.
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
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produto">Os dados do novo produto a ser criado.</param>
        /// <returns>
        /// 201 Created juntamente com o URL do novo recurso se a criação for bem-sucedida.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost(Name = "Produtos")]
        [SwaggerOperation(Summary = "Criação de um novo produto", Description = "Cria um novo produto com base nos dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "O produto foi criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(ProdutoViewModel produto)
        {
            try
            {
                int produtoId = await _produtoPresenter.Create(produto);
                return CreatedAtRoute("ProdutoPorId", new { id = produtoId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Exclui um produto com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do produto a ser excluído.</param>
        /// <returns>
        /// 204 No Content se o produto for excluído com sucesso.
        /// 404 Not Found se o produto não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpDelete("{id}", Name = "ProdutoPorId")]
        [SwaggerOperation(Summary = "Exclusão de produto por ID", Description = "Exclui um produto com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "O produto foi excluído com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _produtoPresenter.Delete(id);

                if (result)
                {
                    return NoContent(); // Retorna 204 No Content se o produto for excluído com sucesso.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o produto não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Obtém um produto com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do produto desejado.</param>
        /// <returns>
        /// 200 OK com o produto recuperado.
        /// 404 Not Found se o produto não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet("{id}", Name = "ProdutoPorId")]
        [SwaggerOperation(Summary = "Obtenção de produto por ID", Description = "Obtém um produto com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O produto foi recuperado com sucesso.", typeof(ProdutoViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var produto = await _produtoPresenter.GetById(id);

                if (produto != null)
                {
                    return Ok(produto); // Retorna 200 OK com o produto recuperado.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o produto não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Atualiza um produto com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do produto a ser atualizado.</param>
        /// <param name="produto">Os dados do produto a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o produto não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut("{id}", Name = "ProdutoPorId")]
        [SwaggerOperation(Summary = "Atualização de um produto por ID", Description = "Atualiza um produto com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O produto foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(int id, ProdutoViewModel produto)
        {
            try
            {
                bool updated = await _produtoPresenter.Update(id, produto);

                if (updated)
                {
                    return Ok(); // Retorna 200 OK se a atualização for bem-sucedida.
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o produto não for encontrado.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

    }
}
