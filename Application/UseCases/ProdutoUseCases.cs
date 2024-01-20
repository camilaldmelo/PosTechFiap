using Application.Interface.UserCases;
using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Repositories;
using Application.Interface.UseCases;

namespace Application.UseCases
{
    public class ProdutoUseCases : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoUserCases _pedidoService;

        public ProdutoUseCases(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork, IPedidoUserCases pedidoService)
        {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
            _pedidoService = pedidoService;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria)
        {
            return await _produtoRepository.GetByIdCategoria(idCategoria);
        }

        public async Task<int> Create(Produto produto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                int produtoId = await _produtoRepository.Create(produto);

                if (produtoId > 0)
                {
                    _unitOfWork.Commit();
                    return produtoId;
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
        public async Task<bool> CanDeleteProduto(int produtoId)
        {
            // Verifique se há pedidos associados a este produto
            var pedidos = await _pedidoService.GetByIdProduto(produtoId);

            if (pedidos != null && pedidos.Any())
            {
                // Se houver pedidos associados, não permita a exclusão do produto
                return false;
            }
            // Se não houver pedidos associados, o produto pode ser excluído
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await CanDeleteProduto(id))
            {
                throw new Exception("Não é possível excluir o produto devido a restrições ou vínculos com outras entidades.");
            }


            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _produtoRepository.Delete(id);
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

        public async Task<Produto> GetById(int id)
        {
            try
            {
                return await _produtoRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Produto produto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                bool updated = await _produtoRepository.Update(produto);

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

    }
}
