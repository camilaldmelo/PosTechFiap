using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        public IProdutoRepository _produtoRepository;
        public IUnitOfWork _unitOfWork;

        public ProdutoService(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
        {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria)
        {
            return await _produtoRepository.GetByIdCategoria(idCategoria);
        }

        public async Task<int> Post(Produto produto)
        {
            produto.Id = await _produtoRepository.GetLastID() + 1;
            try
            {
                _unitOfWork.BeginTransaction();
                await _produtoRepository.Post(produto);
                _unitOfWork.Commit();
                return produto.Id;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
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
