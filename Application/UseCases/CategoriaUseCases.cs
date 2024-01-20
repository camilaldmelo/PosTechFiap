using Application.Interface.UseCases;
using Domain.Entities;
using Domain.Interface.Gateways;
using Domain.Interface.Repositories;

namespace Application.UseCases
{
    public class CategoriaUseCases : ICategoriaUseCases
    {
        private readonly ICategoriaGateways _categoriaRepository;
        private readonly IProdutoUseCases _produtoService;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaUseCases(ICategoriaGateways categoriaRepository, IProdutoUseCases produtoService, IUnitOfWork unitOfWork)
        {
            _categoriaRepository = categoriaRepository;
            _produtoService = produtoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            try
            {
                return await _categoriaRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria> GetById(int id)
        {
            try
            {
                return await _categoriaRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Create(Categoria categoria)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                int categoriaId = await _categoriaRepository.Create(categoria);

                if (categoriaId > 0)
                {
                    _unitOfWork.Commit();
                    return categoriaId;
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

        public async Task<bool> Update(Categoria categoria)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                bool updated = await _categoriaRepository.Update(categoria);

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

        public async Task<bool> CanDeleteCategoria(int categoriaId)
        {
            // Verifique se há produtos associados a esta categoria
            var produtos = await _produtoService.GetByIdCategoria(categoriaId);

            if (produtos != null && produtos.Any())
            {
                // Se houver produtos associados, não permita a exclusão da categoria
                return false;
            }

            // Se não houver produtos associados, a categoria pode ser excluída
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await CanDeleteCategoria(id)){
                throw new Exception("Não é possível excluir a categoria devido a restrições de chave estrangeira.");
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _categoriaRepository.Delete(id);

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
