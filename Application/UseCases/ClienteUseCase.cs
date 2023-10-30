using Application.Interface;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;

namespace Application.UseCases
{
    public class ClienteUseCase : IClienteUseCase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteUseCase(IMapper mapper, IClienteService clienteService)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteViewModel>> GetAll()
        {
            var clientes = await _clienteService.GetAll();

            return _mapper.Map<List<ClienteViewModel>>(clientes);
        }

        public async Task<ClienteViewModel> GetById(int id)
        {
            var cliente = await _clienteService.GetById(id);

            return _mapper.Map<ClienteViewModel>(cliente);
        }
        public async Task<ClienteViewModel> GetByCPF(string cpf)
        {
            var cliente = await _clienteService.GetByCPF(cpf);

            return _mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<int> Create(ClienteViewModel cliente)
        {
            var c = _mapper.Map<Cliente>(cliente);
            try
            {
                return await _clienteService.Create(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, ClienteViewModel cliente)
        {
            var existingCliente = await _clienteService.GetById(id);

            if (existingCliente == null)
            {
                return false; // Cliente não encontrado, portanto, não foi atualizado.
            }

            // Realize as validações necessárias no objeto 'cliente' e manipule exceções, se necessário.

            // Atualize as propriedades do cliente existente com base nos dados de 'cliente'.
            existingCliente.Nome = cliente.Nome;
            existingCliente.CPF = cliente.CPF;
            existingCliente.Email = cliente.Email;
            existingCliente.Data = cliente.Data;

            return await _clienteService.Update(existingCliente); // Atualiza o cliente no serviço.
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _clienteService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
