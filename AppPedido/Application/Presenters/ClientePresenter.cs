using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;
using Application.Interface.UseCases;

namespace Application.Presenters
{
    public class ClientePresenter : IClientePresenter
    {
        private readonly IClienteUseCase _clienteUseCase;
        private readonly IMapper _mapper;

        public ClientePresenter(IMapper mapper, IClienteUseCase clienteService)
        {
            _clienteUseCase = clienteService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteViewModel>> GetAll()
        {
            var clientes = await _clienteUseCase.GetAll();

            return _mapper.Map<List<ClienteViewModel>>(clientes);
        }

        public async Task<ClienteViewModel> GetById(int id)
        {
            var cliente = await _clienteUseCase.GetById(id);

            return _mapper.Map<ClienteViewModel>(cliente);
        }
        public async Task<ClienteViewModel> GetByCPF(string cpf)
        {
            var cliente = await _clienteUseCase.GetByCPF(cpf);

            return _mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<int> Create(ClienteViewModel cliente)
        {
            var c = _mapper.Map<Cliente>(cliente);
            try
            {
                return await _clienteUseCase.Create(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, ClienteViewModel cliente)
        {
            var existingCliente = await _clienteUseCase.GetById(id);

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

            return await _clienteUseCase.Update(existingCliente); // Atualiza o cliente no serviço.
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _clienteUseCase.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
