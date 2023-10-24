
using Application.ViewModel;

namespace Application.Interface
{
    public interface ICategoriaUseCase
    {
        public Task<IEnumerable<CategoriaViewModel>> GetAll();
        public Task<CategoriaViewModel> GetById(int id);
        public Task<int> Create(CategoriaViewModel categoria);
        public Task<bool> Update(int id, CategoriaViewModel categoria);
        public Task<bool> Delete(int id);
    }
}
