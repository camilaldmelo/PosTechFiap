using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoService _pedidoService;

        public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork, IPedidoService pedidoService)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
            _pedidoService = pedidoService;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            try
            {
                return await _clienteRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> GetById(int id)
        {
            try
            {
                return await _clienteRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> GetByCPF(string cpf)
        {
            try
            {
                return await _clienteRepository.GetByCPF(cpf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Create(Cliente cliente)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var clienteCPF = await GetByCPF(cliente.CPF);
                if (clienteCPF != null)
                {
                    throw new Exception("Já existe um usuário cadastrado com esse CPF.");
                }
                
                int clienteId = await _clienteRepository.Create(cliente);

                if (clienteId > 0)
                {
                    _unitOfWork.Commit();
                    return clienteId;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Cliente cliente)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                bool updated = await _clienteRepository.Update(cliente);

                if (updated)
                {
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CanDeleteCliente(int clienteId)
        {
            // Verifique se há pedidos associados a este cliente
            var pedidos = await _pedidoService.GetByIdCliente(clienteId);

            if (pedidos != null && pedidos.Any())
            {
                // Se houver pedidos associados, não permita a exclusão do cliente
                return false;
            }
            // Se não houver pedidos associados, o cliente pode ser excluído
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await CanDeleteCliente(id))
            {
                throw new Exception("Não é possível excluir o cliente devido a restrições ou vínculos com outras entidades.");
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _clienteRepository.Delete(id);

                if (result)
                {
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
}
