using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        public IClienteRepository _clienteRepository;
        public IUnitOfWork _unitOfWork;

        public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Pedido>> GetCliente(string cpf)
        {
            var result = await _clienteRepository.ObterClientePorCpf(cpf);

            return result;
        }

        public async Task<int> PostCliente(Cliente cliente)
        {
            cliente.Data = DateTime.Now;

            try
            {
                _unitOfWork.BeginTransaction();

                cliente.Id = await _clienteRepository.InserirCliente(cliente);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
            }
            return cliente.Id;
        }

    }
}
