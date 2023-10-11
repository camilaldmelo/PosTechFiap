using Application.Interface;
using Application.UseCases;
using Application.ViewModel;
using Domain.Entities;
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
        /// Listagem de produtos
        /// </summary>
        /// <returns>Uma lista de produtos</returns>
        [HttpGet(Name = "Produtos")]
        [SwaggerOperation(Summary = "Listagem de produtos", Description = "Retorna uma lista de produtos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de produtos foi recuperada com sucesso.")]
        [Produces(typeof(IEnumerable<ProdutoViewModel>))]
        public IActionResult GetAll()
        {
            return Ok(_produtoUseCase.GetAll());
        }
    }
}
