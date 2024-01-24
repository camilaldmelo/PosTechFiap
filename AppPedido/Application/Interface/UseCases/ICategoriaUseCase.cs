using Domain.Entities;

namespace Application.Interface.UseCases
{
    public interface ICategoriaUseCase
    {
        public Task<IEnumerable<Categoria>> GetAll();
        public Task<Categoria> GetById(int id);
        public Task<int> Create(Categoria categoria);
        public Task<bool> Update(Categoria categoria);
        public Task<bool> Delete(int id);
    }
}
