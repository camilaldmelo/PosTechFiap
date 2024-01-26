using Application.Interface.UseCases;
using Domain.Entities;
using Domain.Interface.Gateways;

namespace Application.UseCases
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        private readonly ICategoriaGateway _categoriaGateway;
        private readonly IProdutoUseCase _produtoUseCase;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaUseCase(ICategoriaGateway categoriaGateway, IProdutoUseCase produtoUseCase, IUnitOfWork unitOfWork)
        {
            _categoriaGateway = categoriaGateway;
            _produtoUseCase = produtoUseCase;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            try
            {
                return await _categoriaGateway.GetAll();
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
                return await _categoriaGateway.GetById(id);
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
                int categoriaId = await _categoriaGateway.Create(categoria);

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

        public async Task<bool> Update(Categoria categoria, int id)
        {
            try
            {
                var existingCategoria = await _categoriaGateway.GetById(id);

                if (existingCategoria == null)
                {
                    return false; // Categoria não encontrada, portanto, não foi atualizada.
                }

                // Realize as validações necessárias no objeto 'categoria' e manipule exceções, se necessário.

                // Atualize as propriedades da categoria existente com base nos dados de 'categoria'.
                existingCategoria.Nome = categoria.Nome;

                _unitOfWork.BeginTransaction();
                bool updated = await _categoriaGateway.Update(existingCategoria);

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
            var produtos = await _produtoUseCase.GetByIdCategoria(categoriaId);

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
                var result = await _categoriaGateway.Delete(id);

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
