using Application.ViewModel;

namespace Application.Interface
{
    public interface IClienteUseCase
    {
        public Task<IEnumerable<ClienteViewModel>> GetCliente(string cpf);


        public Task<int> PostCliente(ClienteViewModel cliente);
    }
}
