using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
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
                var clienteCPF = await _clienteRepository.GetByCPF(cliente.CPF);
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
            // Adicione lógica aqui para verificar se há alguma restrição que impeça a exclusão do acompanhamento, se necessário.

            // Se não houver restrições, o acompanhamento pode ser excluído.
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
