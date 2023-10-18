using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.ValueObjects;

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
            catch (Exception e) 
            {
                _unitOfWork.Rollback();
                throw new Exception(e.Message);
            }
            
        }
    }
}
