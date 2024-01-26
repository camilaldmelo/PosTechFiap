using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Presenters
{
    public class CategoriaPresenter : ICategoriaPresenter
    {
        private readonly IMapper _mapper;

        public CategoriaPresenter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaViewModel>> ConvertToListViewModel(IEnumerable<Categoria> categorias)
        {
            return await Task.Run(() => _mapper.Map<List<CategoriaViewModel>>(categorias));
        }

        public async Task<CategoriaViewModel> ConvertToViewModel(Categoria categoria)
        {
            return await Task.Run(() => _mapper.Map<CategoriaViewModel>(categoria));
        }

        public async Task<IEnumerable<Categoria>> ConvertFromListViewModel(IEnumerable<CategoriaViewModel> categorias)
        {
            return await Task.Run(() => _mapper.Map<List<Categoria>>(categorias));
        }

        public async Task<Categoria> ConvertFromViewModel(CategoriaViewModel categoria)
        {
            return await Task.Run(() => _mapper.Map<Categoria>(categoria));
        }
    }
}
