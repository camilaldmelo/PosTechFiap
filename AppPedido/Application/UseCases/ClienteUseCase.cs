using Domain.Entities;
using Domain.Interface.Gateways;
using Application.Interface.UseCases;

namespace Application.UseCases
{
    public class ClienteUseCase : IClienteUseCase
    {
        private readonly IClienteGateway _clienteGateway;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoUseCase _pedidoUseCase;

        public ClienteUseCase(IClienteGateway clienteGateway, IUnitOfWork unitOfWork, IPedidoUseCase pedidoUseCase)
        {
            _clienteGateway = clienteGateway;
            _unitOfWork = unitOfWork;
            _pedidoUseCase = pedidoUseCase;
        }
        
        public async Task<IEnumerable<Cliente>> GetAll()
        {
            try
            {
                return await _clienteGateway.GetAll();
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
                return await _clienteGateway.GetById(id);
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
                return await _clienteGateway.GetByCPF(cpf);
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
                
                int clienteId = await _clienteGateway.Create(cliente);

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

        public async Task<bool> Update(Cliente cliente, int id)
        {
            try
            {
                var existingCliente = await _clienteGateway.GetById(id);

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

                _unitOfWork.BeginTransaction();
                bool updated = await _clienteGateway.Update(existingCliente);

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
            var pedidos = await _pedidoUseCase.GetByIdCliente(clienteId);

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
                var result = await _clienteGateway.Delete(id);

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
