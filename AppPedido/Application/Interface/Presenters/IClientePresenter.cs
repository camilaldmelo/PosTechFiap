using Application.Presenters.ViewModel;
using Domain.Entities;

namespace Application.Interface.Presenters
{
    public interface IClientePresenter
    {
        public Task<IEnumerable<ClienteViewModel>> ConvertToListViewModel(IEnumerable<Cliente> clientes);

        public Task<ClienteViewModel> ConvertToViewModel(Cliente cliente);

        public Task<IEnumerable<Cliente>> ConvertFromListViewModel(IEnumerable<ClienteViewModel> clientes);

        public Task<Cliente> ConvertFromViewModel(ClienteViewModel cliente);
    }
}
