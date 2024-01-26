using Application.Interface.UseCases;
using Domain.Entities;
using Domain.Interface.Gateways;

namespace Application.UseCases
{
    public class ProdutoUseCase : IProdutoUseCase
    {
        private readonly IProdutoGateway _produtoGateway;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoUseCase _pedidoUseCase;

        public ProdutoUseCase(IProdutoGateway produtoGateway, IUnitOfWork unitOfWork, IPedidoUseCase pedidoUseCase)
        {
            _produtoGateway = produtoGateway;
            _unitOfWork = unitOfWork;
            _pedidoUseCase = pedidoUseCase;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoGateway.GetAll();
        }

        public async Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria)
        {
            return await _produtoGateway.GetByIdCategoria(idCategoria);
        }

        public async Task<int> Create(Produto produto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                int produtoId = await _produtoGateway.Create(produto);

                if (produtoId > 0)
                {
                    _unitOfWork.Commit();
                    return produtoId;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> CanDeleteProduto(int produtoId)
        {
            // Verifique se há pedidos associados a este produto
            var pedidos = await _pedidoUseCase.GetByIdProduto(produtoId);

            if (pedidos != null && pedidos.Any())
            {
                // Se houver pedidos associados, não permita a exclusão do produto
                return false;
            }
            // Se não houver pedidos associados, o produto pode ser excluído
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await CanDeleteProduto(id))
            {
                throw new Exception("Não é possível excluir o produto devido a restrições ou vínculos com outras entidades.");
            }


            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _produtoGateway.Delete(id);
                if (result)
                {
                    _unitOfWork.Commit();
                    return true; 
                }
                else
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetById(int id)
        {
            try
            {
                return await _produtoGateway.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Produto produto, int id)
        {
            try
            {
                var existingProduct = await _produtoGateway.GetById(id);

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
                existingProduct.Categoria = produto.Categoria;

                _unitOfWork.BeginTransaction();
                bool updated = await _produtoGateway.Update(existingProduct);

                if (updated)
                {
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

    }
}
