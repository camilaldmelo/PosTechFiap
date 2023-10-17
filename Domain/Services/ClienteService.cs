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
            var idCliente = await _clienteRepository.ObterIdUltimoRegistroInserido() + 1;
            cliente.Id = idCliente;
            cliente.Data = DateTime.Now;

            try
            {
                _unitOfWork.BeginTransaction();

                await _clienteRepository.Inserircliente(cliente);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
            }
            return idCliente;
        }

    }
}
