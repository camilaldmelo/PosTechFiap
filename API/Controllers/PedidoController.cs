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
        public IEnumerable<PedidoViewModel> Get(int idAcompanhamento)
        {
            return _pedidoUseCase.GetPedido(idAcompanhamento);
        }

        [HttpPost(Name = "Pedido")]
        public int Post(PedidoViewModel pedido)
        {
            return _pedidoUseCase.PostPedido(pedido);
        }

        [HttpPut(Name = "Pedido")]
        public bool Put(PedidoViewModel pedido)
        {
            return _pedidoUseCase.PutPedido(pedido);
        }
    }
}
