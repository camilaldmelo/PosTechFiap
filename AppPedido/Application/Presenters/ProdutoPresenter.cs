using Application.Interface.Presenters;
using Application.Interface.UseCases;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Presenters
{
    public class ProdutoPresenter : IProdutoPresenter
    {
        public IProdutoUseCase _produtoUseCase;
        private readonly IMapper _mapper;

        public ProdutoPresenter(IMapper mapper, IProdutoUseCase produtoUseCase)
        {
            _produtoUseCase = produtoUseCase;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ConvertToListViewModel(IEnumerable<Produto> produtos)
        {
            return await Task.Run(() => _mapper.Map<List<ProdutoViewModel>>(produtos));
        }

        public async Task<ProdutoViewModel> ConvertToViewModel(Produto produto)
        {
            return await Task.Run(() => _mapper.Map<ProdutoViewModel>(produto));
        }

        public async Task<IEnumerable<Produto>> ConvertFromListViewModel(IEnumerable<ProdutoViewModel> produtos)
        {
            return await Task.Run(() => _mapper.Map<List<Produto>>(produtos));
        }

        public async Task<Produto> ConvertFromViewModel(ProdutoViewModel produto)
        {
            return await Task.Run(() => {
                var produtoModel = _mapper.Map<Produto>(produto);
                produtoModel.Categoria = new Categoria(produto.IdCategoria, produto.Categoria);
                return produtoModel; 
            });
        }
    }
}
