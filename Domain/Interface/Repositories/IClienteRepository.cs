using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IClienteRepository
    {
        public Task<IEnumerable<Cliente>> ObterClientePorCpf(string cpf);

        public Task<int> InserirCliente(Cliente cliente);

        public Task<IEnumerable<Cliente>> GetAll();
        public Task<Cliente> GetById(int id);
        public Task<Cliente> GetByCPF(string cpf);
        public Task<int> Create(Cliente cliente);
        public Task<bool> Update(Cliente cliente);
        public Task<bool> Delete(int id);
    }
}
