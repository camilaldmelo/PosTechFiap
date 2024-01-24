using Domain.Entities;
using Domain.Interface.Gateways;
using Application.Interface.UseCases;

namespace Application.UseCases
{
    public class AcompanhamentoUseCase : IAcompanhamentoUseCase
    {
        private readonly IAcompanhamentoGateway _acompanhamentoGateway;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoUseCase _pedidoUseCase;

        public AcompanhamentoUseCase(IAcompanhamentoGateway acompanhamentoGateway, 
                                     IUnitOfWork unitOfWork,
                                     IPedidoUseCase pedidoUseCase)
        {
            _acompanhamentoGateway = acompanhamentoGateway;
            _unitOfWork = unitOfWork;
            _pedidoUseCase = pedidoUseCase;
        }

        public async Task<IEnumerable<Acompanhamento>> GetAll()
        {
            try
            {
                return await _acompanhamentoGateway.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Acompanhamento> GetById(int id)
        {
            try
            {
                return await _acompanhamentoGateway.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Create(Acompanhamento acompanhamento)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                int acompanhamentoId = await _acompanhamentoGateway.Create(acompanhamento);

                if (acompanhamentoId > 0)
                {
                    _unitOfWork.Commit();
                    return acompanhamentoId;
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

        public async Task<bool> Update(Acompanhamento acompanhamento)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                bool updated = await _acompanhamentoGateway.Update(acompanhamento);

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

        public async Task<bool> CanDeleteAcompanhamento(int acompanhamentoId)
        {
            // Verifique se há pedidos associados a esta acompanhamento/status
            var pedidos = await _pedidoUseCase.GetByIdStatus(acompanhamentoId);

            if (pedidos != null && pedidos.Any())
            {
                // Se houver pedidos associados, não permita a exclusão da acompanhamento/status
                return false;
            }
            // Se não houver pedidos associados, o acompanhamento/status pode ser excluído
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await CanDeleteAcompanhamento(id))
            {
                throw new Exception("Não é possível excluir o acompanhamento devido a restrições ou vínculos com outras entidades.");
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _acompanhamentoGateway.Delete(id);

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
