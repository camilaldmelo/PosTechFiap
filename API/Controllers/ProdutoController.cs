using Application.Interface;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : Controller
    {

        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoUseCase _produtoUseCase;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoUseCase produtoUseCase)
        {
            _logger = logger;
            _produtoUseCase = produtoUseCase;
        }

        /// <summary>
        /// Obtém uma lista de todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        [HttpGet(Name = "Produtos")]
        [SwaggerOperation(Summary = "Listagem de todos os produtos", Description = "Recupera uma lista de todos os produtos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de produtos foi recuperada com sucesso.", typeof(IEnumerable<ProdutoViewModel>))]
        public async Task<IEnumerable<ProdutoViewModel>> GetAll()
        {
            return await _produtoUseCase.GetAll();
        }


        /// <summary>
        /// Obtém uma lista de produtos com base no identificador da categoria.
        /// </summary>
        /// <param name="idCategoria">O identificador da categoria dos produtos desejada.</param>
        /// <returns>Uma lista de produtos filtrados pela categoria especificada.</returns>
        [HttpGet("{idCategoria}", Name = "ProdutosPorCategoria")]
        [SwaggerOperation(Summary = "Listagem de produtos por categoria", Description = "Recupera uma lista de produtos filtrados pela categoria especificada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de produtos foi recuperada com sucesso.", typeof(IEnumerable<ProdutoViewModel>))]
        public async Task<IEnumerable<ProdutoViewModel>> GetByIdCategoria(int idCategoria)
        {
            return await _produtoUseCase.GetByIdCategoria(idCategoria);
        }

        [HttpPost(Name = "Produtos")]
        public async Task<int> Post(ProdutoViewModel produto)
        {
            return await _produtoUseCase.Post(produto);
        }
    }
}
