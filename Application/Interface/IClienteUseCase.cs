using Application.ViewModel;

namespace Application.Interface
{
    public interface IClienteUseCase
    {
        public Task<IEnumerable<ClienteViewModel>> GetAll();
        public Task<ClienteViewModel> GetById(int id);
        public Task<ClienteViewModel> GetByCPF(string cpf);
        public Task<int> Create(ClienteViewModel cliente);
        public Task<bool> Update(int id, ClienteViewModel cliente);
        public Task<bool> Delete(int id);
    }
}
