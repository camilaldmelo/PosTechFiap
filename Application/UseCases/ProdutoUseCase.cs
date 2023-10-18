using Application.Interface;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using Domain.Services;

namespace Application.UseCases
{
    public class ProdutoUseCase : IProdutoUseCase
    {
        public IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoUseCase(IMapper mapper, IProdutoService produtoService)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetAll()
        {
            var produtos = await _produtoService.GetAll();

            return _mapper.Map<List<ProdutoViewModel>>(produtos);
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetByIdCategoria(int idCategoria)
        {
            var produtos = await _produtoService.GetByIdCategoria(idCategoria);

            return _mapper.Map<List<ProdutoViewModel>>(produtos);
        }

        public async Task<int> Post(ProdutoViewModel produto)
        {
            var p = _mapper.Map<Produto>(produto);

            return await _produtoService.Post(p);
        }
    }
}
