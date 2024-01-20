using Application.Interface.Presenters;
using Application.Interface.UserCases;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Presenters
{
    public class ProdutoUseCase : IProdutoPresenters
    {
        public IProdutoUserCases _produtoService;
        private readonly IMapper _mapper;

        public ProdutoUseCase(IMapper mapper, IProdutoUserCases produtoService)
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

        public async Task<int> Create(ProdutoViewModel produto)
        {
            var p = _mapper.Map<Produto>(produto);
            try
            {
                return await _produtoService.Create(p);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _produtoService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoViewModel> GetById(int id)
        {
            var produto = await _produtoService.GetById(id);

            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public async Task<bool> Update(int id, ProdutoViewModel produto)
        {
            var existingProduct = await _produtoService.GetById(id);

            if (existingProduct == null)
            {
                return false; // Produto não encontrado, portanto, não foi atualizado.
            }

            // Realize as validações necessárias no objeto 'produto' e manipule exceções, se necessário.

            // Atualize as propriedades do produto existente com base nos dados de 'produto'.
            existingProduct.Nome = produto.Nome;
            existingProduct.Preco = produto.Preco;
            existingProduct.Descricao = produto.Descricao;
            existingProduct.UrlImagem = produto.UrlImagem;
            existingProduct.Categoria = new Categoria(produto.IdCategoria, produto.Categoria);
            return await _produtoService.Update(existingProduct); // Atualiza o produto no serviço.
        }
    }
}
