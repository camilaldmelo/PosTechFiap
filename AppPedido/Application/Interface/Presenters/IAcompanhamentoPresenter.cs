using Application.Presenters.ViewModel;
using Domain.Entities;

namespace Application.Interface.Presenters
{
    public interface IAcompanhamentoPresenter
    {
        public Task<IEnumerable<AcompanhamentoViewModel>> ConvertToListViewModel(IEnumerable<Acompanhamento> acompanhamentos);

        public Task<AcompanhamentoViewModel> ConvertToViewModel(Acompanhamento acompanhamento);

        public Task<IEnumerable<Acompanhamento>> ConvertFromListViewModel(IEnumerable<AcompanhamentoViewModel> acompanhamentos);

        public Task<Acompanhamento> ConvertFromViewModel(AcompanhamentoViewModel acompanhamento);
    }
}
