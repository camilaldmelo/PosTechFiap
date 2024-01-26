using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Presenters
{
    public class AcompanhamentoPresenter : IAcompanhamentoPresenter
    {
        private readonly IMapper _mapper;

        public AcompanhamentoPresenter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<AcompanhamentoViewModel>> ConvertToListViewModel(IEnumerable<Acompanhamento> acompanhamentos)
        {
            return await Task.Run(() => _mapper.Map<List<AcompanhamentoViewModel>>(acompanhamentos));
        }

        public async Task<AcompanhamentoViewModel> ConvertToViewModel(Acompanhamento acompanhamento)
        {
            return await Task.Run(() => _mapper.Map<AcompanhamentoViewModel>(acompanhamento));
        }

        public async Task<IEnumerable<Acompanhamento>> ConvertFromListViewModel(IEnumerable<AcompanhamentoViewModel> acompanhamentos)
        {
            return await Task.Run(() => _mapper.Map<List<Acompanhamento>>(acompanhamentos));
        }

        public async Task<Acompanhamento> ConvertFromViewModel(AcompanhamentoViewModel acompanhamento)
        {
            return await Task.Run(() => _mapper.Map<Acompanhamento>(acompanhamento));
        }
    }
}
