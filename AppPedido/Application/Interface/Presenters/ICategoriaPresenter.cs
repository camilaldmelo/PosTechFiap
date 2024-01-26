using Application.Presenters.ViewModel;
using Domain.Entities;

namespace Application.Interface.Presenters
{
    public interface ICategoriaPresenter
    {
        public Task<IEnumerable<CategoriaViewModel>> ConvertToListViewModel(IEnumerable<Categoria> categorias);

        public Task<CategoriaViewModel> ConvertToViewModel(Categoria categoria);

        public Task<IEnumerable<Categoria>> ConvertFromListViewModel(IEnumerable<CategoriaViewModel> categorias);

        public Task<Categoria> ConvertFromViewModel(CategoriaViewModel categoria);
    }
}
