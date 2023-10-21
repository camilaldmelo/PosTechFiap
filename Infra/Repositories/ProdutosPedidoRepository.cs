using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;

namespace Infra.Repositories
{
    public class ProdutosPedidoRepository : IProdutosPedidoRepository
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
            string commandText = "SELECT id as Id, id_pedido as IdPedido, id_produto as IdProduto, quantidade as Quantidade FROM public.tbl_produtos_pedido WHERE id_pedido = (@idPedido)";
            var parameters = new { idPedido };

            return await _session.Connection.QueryAsync<ProdutosPedido>(commandText, parameters);
        }
    }
}
