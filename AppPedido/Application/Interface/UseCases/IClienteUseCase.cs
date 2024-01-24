using Domain.Entities;

namespace Application.Interface.UseCases
{
    public interface IClienteUseCase
    {
        public Task<IEnumerable<Cliente>> GetAll();
        public Task<Cliente> GetById(int id);
        public Task<Cliente> GetByCPF(string cpf);
        public Task<int> Create(Cliente cliente);
        public Task<bool> Update(Cliente cliente);
        public Task<bool> Delete(int id);
    }
}
