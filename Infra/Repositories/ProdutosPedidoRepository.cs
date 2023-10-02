using Domain.Entities;
using Domain.Interface.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infra.Repositories
{
    public class ProdutosPedidoRepository : RepositoryBase, IProdutosPedidoRepository
    {
        public ProdutosPedidoRepository(IConfiguration config) : base(config) { }

        public bool DeletarProdutoPedidoPorIdPedido(int idPedido)
        {
            return true;
            //using var cmd = new NpgsqlCommand("DELETE FROM TBL_PRODUTOS_PEDIDO WHERE ID = ($1)", ObterConexaoExclusiva())
            //{
            //    Parameters = { new() { Value = idPedido } }
            //};

            //return cmd.ExecuteNonQuery() > 0;
        }

        public int InserirProdutoPedido(ProdutosPedido produtosPedido)
        {
            return 99;
            //using var cmd = new NpgsqlCommand(" INSERT INTO TBL_PRODUTOS_PEDIDO (ID_PEDIDO, ID_PRODUTO, QUANTIDADE) VALUES ($1), ($2), ($3);" +
            //                                  " SELECT currval(pg_get_serial_sequence('TBL_PRODUTOS_PEDIDO','ID_PEDIDO'));", ObterConexaoExclusiva())
            //{
            //    Parameters =
            //    {
            //        new() { Value = produtosPedido.IdPedido },
            //        new() { Value = produtosPedido.IdProduto },
            //        new() { Value = produtosPedido.Quantidade }
            //    }
            //};

            //return cmd.ExecuteNonQuery();
        }

        public IEnumerable<ProdutosPedido> ObterProdutoPedidoPorPedido(int idPedido)
        {
            return new List<ProdutosPedido>();
        }
    }
}
