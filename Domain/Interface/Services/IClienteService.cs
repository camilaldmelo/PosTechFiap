using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IClienteService
    {
        public Task<IEnumerable<Pedido>> GetCliente(string cpf);

        public Task<int> PostCliente(Cliente cliente);
    }
}
