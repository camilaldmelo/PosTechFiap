using Application.Interface;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;

namespace Application.UseCases
{
    public class ClienteUseCase : IClienteUseCase
    {
        public IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteUseCase(IMapper mapper, IClienteService clienteService)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteViewModel>> GetCliente(string cpf)
        {
            var cliente = await _clienteService.GetCliente(cpf);

            return _mapper.Map<List<ClienteViewModel>>(cpf);
        }

        public async Task<int> PostCliente(ClienteViewModel clienteViewModel)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);

            return await _clienteService.PostCliente(cliente);
        }

    }
}
