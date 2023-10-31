using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class AcompanhamentoService : IAcompanhamentoService
    {
        private readonly IAcompanhamentoRepository _acompanhamentoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoService _pedidoService;

        public AcompanhamentoService(IAcompanhamentoRepository acompanhamentoRepository, 
                                     IUnitOfWork unitOfWork,
                                     IPedidoService pedidoService)
        {
            _acompanhamentoRepository = acompanhamentoRepository;
            _unitOfWork = unitOfWork;
            _pedidoService = pedidoService;
        }

        public async Task<IEnumerable<Acompanhamento>> GetAll()
        {
            try
            {
                return await _acompanhamentoRepository.GetAll();
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
                return await _acompanhamentoRepository.GetById(id);
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
                int acompanhamentoId = await _acompanhamentoRepository.Create(acompanhamento);

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
                bool updated = await _acompanhamentoRepository.Update(acompanhamento);

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
            var pedidos = await _pedidoService.GetByIdStatus(acompanhamentoId);

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
                var result = await _acompanhamentoRepository.Delete(id);

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
