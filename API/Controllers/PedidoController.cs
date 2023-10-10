using Application.Interface;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet(Name = "Pedido")]
        public async Task<IEnumerable<PedidoViewModel>> Get(int idPedido)
        {
            return await _pedidoUseCase.GetPedido(idPedido);
        }

        [HttpPost(Name = "Pedido")]
        public async Task<int> Post(PedidoViewModel pedido)
        {
            return await _pedidoUseCase.PostPedido(pedido);
        }

        [HttpPut(Name = "Pedido")]
        public async Task<bool> Put(PedidoViewModel pedido)
        {
            return await _pedidoUseCase.PutPedido(pedido);
        }
    }
}
