using Application.ViewModel;

namespace Application.Interface
{
    public interface IProdutoUseCase
    {
        public Task<IEnumerable<ProdutoViewModel>> GetAll();
        public Task<IEnumerable<ProdutoViewModel>> GetByIdCategoria(int idCategoria);
        public Task<int> Post(ProdutoViewModel produto);
    }
}
