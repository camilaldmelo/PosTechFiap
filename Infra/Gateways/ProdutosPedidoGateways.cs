using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Infra.DB;

namespace Infra.Gateways
{
    public class ProdutosPedidoRepository : IProdutosPedidoGateways
    {
        private RepositoryBase _session;

        public ProdutosPedidoRepository(RepositoryBase session)
        {
            _session = session;
        }

        /// <summary>
        /// Deleta produto e pedido por ID do pedido
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdPedido(int idPedido)
        {
            string sql = "DELETE FROM public.tbl_produtos_pedido WHERE id_pedido = (@idPedido)";
            var parametros = new { idPedido };

            await _session.Connection.ExecuteAsync(sql, parametros, _session.Transaction);
            return true;
        }

        /// <summary>
        /// Insere produto e pedido
        /// </summary>
        /// <param name="produtosPedido"></param>
        /// <returns></returns>
        public async Task<bool> Create(ProdutosPedido produtosPedido)
        {
            string sql = "INSERT INTO public.tbl_produtos_pedido (id_pedido, id_produto, quantidade) VALUES (@IdPedido, @IdProduto, @Quantidade);";

            var parameters = new
            {
                produtosPedido.IdPedido,
                produtosPedido.IdProduto,
                produtosPedido.Quantidade
            };

            await _session.Connection.ExecuteAsync(sql, parameters, _session.Transaction);
            return true;
        }

        public async Task<IEnumerable<ProdutosPedido>> GetByIdPedido(int idPedido)
        {
            string commandText = @"SELECT pp.id, 
		                                  pp.id_pedido as IdPedido, 
		                                  pp.id_produto as IdProduto, 
		                                  pp.quantidade,
		                                  p.id,
		                                  p.descricao,
		                                  p.id_categoria as IdCategoria,
		                                  p.nome,
		                                  p.preco,
		                                  p.url_imagem as UrlImagem
                                     FROM public.tbl_produtos_pedido pp LEFT JOIN
     	                                  public.tbl_produto p ON p.id = pp.id_produto 
                                    WHERE id_pedido = (@idPedido)";

            var produtosPedidos = await _session.Connection.QueryAsync<ProdutosPedido, Produto, ProdutosPedido>(
                sql: commandText,
                map: (produtosPedido, produto) =>
                {
                    produtosPedido.Produto = produto;
                    return produtosPedido;
                },
                splitOn: "Id",
                param: new { idPedido });
            return produtosPedidos;
        }
    }
}
