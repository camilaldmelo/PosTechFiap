using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IClienteRepository
    {
        public Task<IEnumerable<Pedido>> ObterClientePorCpf(string cpf);

        public Task<int> InserirCliente(Cliente cliente);
    }
}
