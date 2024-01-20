using Application.Presenters.ViewModel;

namespace Application.Interface.Presenters
{
    public interface IProdutoPresenters
    {
        public Task<IEnumerable<ProdutoViewModel>> GetAll();
        public Task<IEnumerable<ProdutoViewModel>> GetByIdCategoria(int idCategoria);
        public Task<int> Create(ProdutoViewModel produto);
        public Task<bool> Delete(int id);
        public Task<ProdutoViewModel> GetById(int id);
        public Task<bool> Update(int id, ProdutoViewModel produto);
    }
}
