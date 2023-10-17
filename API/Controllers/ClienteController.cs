using Application.Interface;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteUseCase _clienteUseCase;

        public ClienteController(IClienteUseCase clienteUseCase)
        {
            _clienteUseCase = clienteUseCase;
        }

        [HttpGet(Name = "Cliente")]
        public async Task<IEnumerable<ClienteViewModel>> Get(string cpf)
        {
            return await _clienteUseCase.GetCliente(cpf);
        }

        [HttpPost(Name = "Cliente")]
        public async Task<int> Post(ClienteViewModel cliente)
        {
            return await _clienteUseCase.PostCliente(cliente);
        }

    }
}
