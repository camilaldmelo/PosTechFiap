using Domain.Entities;

namespace Application.Interface.UseCases
{
    public interface IProdutoUseCases
    {
        public Task<IEnumerable<Produto>> GetAll();
        public Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria);
        public Task<int> Create(Produto produto);
        public Task<bool> Delete(int id);
        public Task<Produto> GetById(int id);
        public Task<bool> Update(Produto produto);
    }
}
