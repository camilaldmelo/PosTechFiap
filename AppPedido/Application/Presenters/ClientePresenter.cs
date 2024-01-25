using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Presenters
{
    public class ClientePresenter : IClientePresenter
    {
        private readonly IMapper _mapper;

        public ClientePresenter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteViewModel>> ConvertToListViewModel(IEnumerable<Cliente> clientes)
        {
            return await Task.Run(() => _mapper.Map<List<ClienteViewModel>>(clientes));
        }

        public async Task<ClienteViewModel> ConvertToViewModel(Cliente cliente)
        {
            return await Task.Run(() => _mapper.Map<ClienteViewModel>(cliente));
        }

        public async Task<IEnumerable<Cliente>> ConvertFromListViewModel(IEnumerable<ClienteViewModel> clientes)
        {
            return await Task.Run(() => _mapper.Map<List<Cliente>>(clientes));
        }

        public async Task<Cliente> ConvertFromViewModel(ClienteViewModel cliente)
        {
            return await Task.Run(() => _mapper.Map<Cliente>(cliente));
        }
    }
}
