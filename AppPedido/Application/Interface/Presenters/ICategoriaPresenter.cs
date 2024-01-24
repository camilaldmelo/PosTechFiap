using Application.Presenters.ViewModel;

namespace Application.Interface.Presenters
{
    public interface ICategoriaPresenter
    {
        public Task<IEnumerable<CategoriaViewModel>> GetAll();
        public Task<CategoriaViewModel> GetById(int id);
        public Task<int> Create(CategoriaViewModel categoria);
        public Task<bool> Update(int id, CategoriaViewModel categoria);
        public Task<bool> Delete(int id);
    }
}
