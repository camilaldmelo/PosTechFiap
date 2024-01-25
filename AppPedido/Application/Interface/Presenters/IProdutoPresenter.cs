using Application.Presenters.ViewModel;
using Domain.Entities;

namespace Application.Interface.Presenters
{
    public interface IProdutoPresenter
    {
        public Task<IEnumerable<ProdutoViewModel>> ConvertToListViewModel(IEnumerable<Cliente> produtos);

        public Task<ProdutoViewModel> ConvertToViewModel(Produto produto);

        public Task<IEnumerable<Produto>> ConvertFromListViewModel(IEnumerable<ProdutoViewModel> produtos);

        public Task<Produto> ConvertFromViewModel(ProdutoViewModel produto);
    }
}
