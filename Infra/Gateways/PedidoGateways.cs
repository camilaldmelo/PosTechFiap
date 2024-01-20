using Dapper;
using Domain.Entities;
using Domain.Interface.Gateways;
using Infra.DB;

namespace Infra.Gateways
{
    public class PedidoGateways : IPedidoRepository
    {
        private RepositoryDB _session;

        public PedidoGateways(RepositoryDB session)
        {
            _session = session;
        }

        public async Task<bool> Update(Pedido pedido)
        {
            var sql = $@"UPDATE public.tbl_pedido
                            SET id_cliente=@IdCliente, id_acompanhamento=@IdAcompanhamento
                          WHERE id=@Id;";

            var parameters = new
            {
                pedido.Id,
                pedido.IdCliente,
                pedido.IdAcompanhamento
            };

            var ret = await _session.Connection.ExecuteAsync(sql, parameters, _session.Transaction);
            return ret > 0;
        }

        public async Task<int> Create(Pedido pedido)
        {
            string sql = "INSERT INTO public.tbl_pedido (id_cliente, data, id_acompanhamento) VALUES (@IdCliente, @Data, @IdAcompanhamento) RETURNING id";

            var parameters = new
            {
                pedido.IdCliente,
                pedido.Data,
                pedido.IdAcompanhamento
            };
            return await _session.Connection.ExecuteScalarAsync<int>(sql, parameters, _session.Transaction);
        }

        public async Task<int> GetIdLastRecordInserted()
        {
            string commandText = "SELECT id FROM public.tbl_pedido ORDER BY id DESC LIMIT 1";
            return await _session.Connection.QueryFirstOrDefaultAsync<int>(commandText);
        }

        public async Task<Pedido> GetById(int idPedido)
        {
            string commandText = @" SELECT p.id, 
                                           p.id_cliente as IdCliente, 
                                           p.data, 
                                           p.id_acompanhamento as IdAcompanhamento,
                                           c.id,
                                           c.nome,
                                           c.cpf,
                                           c.email,
                                           a.id,
                                           a.nome
                                      FROM public.tbl_pedido p LEFT JOIN
                                           public.tbl_cliente c ON c.id = p.id_cliente LEFT join
                                           public.tbl_acompanhamento a ON a.id = p.id_acompanhamento
                                     WHERE p.id = @idPedido";

            var pedidos = await _session.Connection.QueryAsync<Pedido, Cliente, Acompanhamento, Pedido>(
                sql: commandText,
                map: (pedido, cliente, acompanhamento) =>
                {
                    pedido.Cliente = cliente;
                    pedido.Acompanhamento = acompanhamento;
                    return pedido;
                },
                splitOn: "Id",
                param: new { idPedido });
            return pedidos.FirstOrDefault();
        }

        public async Task<IEnumerable<Pedido>> GetByIdStatus(int idAcompanhamento)
        {
            string commandText = @" SELECT p.id, 
                                           p.id_cliente as IdCliente, 
                                           p.data, 
                                           p.id_acompanhamento as IdAcompanhamento,
                                           c.id,
                                           c.nome,
                                           c.cpf,
                                           c.email,
                                           a.id,
                                           a.nome
                                      FROM public.tbl_pedido p LEFT JOIN
                                           public.tbl_cliente c ON c.id = p.id_cliente LEFT join
                                           public.tbl_acompanhamento a ON a.id = p.id_acompanhamento
                                     WHERE p.id_acompanhamento = @idAcompanhamento";

            var pedidos = await _session.Connection.QueryAsync<Pedido, Cliente, Acompanhamento, Pedido>(
                sql: commandText,
                map: (pedido, cliente, acompanhamento) =>
                {
                    pedido.Cliente = cliente;
                    pedido.Acompanhamento = acompanhamento;
                    return pedido;
                },
                splitOn: "Id",
                param: new { idAcompanhamento });
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetInProgress()
        {
            string commandText = @" SELECT p.id, 
                                           p.id_cliente as IdCliente, 
                                           p.data, 
                                           p.id_acompanhamento as IdAcompanhamento,
                                           c.id,
                                           c.nome,
                                           c.cpf,
                                           c.email,
                                           a.id,
                                           a.nome
                                      FROM public.tbl_pedido p LEFT JOIN
                                           public.tbl_cliente c ON c.id = p.id_cliente LEFT join
                                           public.tbl_acompanhamento a ON a.id = p.id_acompanhamento
                                     WHERE p.id_acompanhamento in (2,3,4)";

            var pedidos = await _session.Connection.QueryAsync<Pedido, Cliente, Acompanhamento, Pedido>(
                sql: commandText,
                map: (pedido, cliente, acompanhamento) =>
                {
                    pedido.Cliente = cliente;
                    pedido.Acompanhamento = acompanhamento;
                    return pedido;
                },
                splitOn: "Id");
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByIdCliente(int idCliente)
        {
            string commandText = @" SELECT p.id, 
                                           p.id_cliente as IdCliente, 
                                           p.data, 
                                           p.id_acompanhamento as IdAcompanhamento,
                                           c.id,
                                           c.nome,
                                           c.cpf,
                                           c.email,
                                           a.id,
                                           a.nome
                                      FROM public.tbl_pedido p LEFT JOIN
                                           public.tbl_cliente c ON c.id = p.id_cliente LEFT join
                                           public.tbl_acompanhamento a ON a.id = p.id_acompanhamento
                                     WHERE p.id_cliente = @idCliente";

            var pedidos = await _session.Connection.QueryAsync<Pedido, Cliente, Acompanhamento, Pedido>(
                sql: commandText,
                map: (pedido, cliente, acompanhamento) =>
                {
                    pedido.Cliente = cliente;
                    pedido.Acompanhamento = acompanhamento;
                    return pedido;
                },
                splitOn: "Id",
                param: new { idCliente });
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByIdProduto(int produtoId)
        {
            string commandText = @" SELECT p.id, 
                                           p.id_cliente as IdCliente, 
                                           p.data, 
                                           p.id_acompanhamento as IdAcompanhamento,
                                           c.id,
                                           c.nome,
                                           c.cpf,
                                           c.email,
                                           a.id,
                                           a.nome
                                      FROM public.tbl_pedido p LEFT JOIN
                                           public.tbl_cliente c ON c.id = p.id_cliente LEFT join
                                           public.tbl_acompanhamento a ON a.id = p.id_acompanhamento
                                     WHERE p.id IN (
                                                        SELECT pp.ID_PEDIDO
                                                        FROM TBL_PRODUTOS_PEDIDO pp
                                                        WHERE pp.ID_PRODUTO = @produtoId
                                                    );";

            var pedidos = await _session.Connection.QueryAsync<Pedido, Cliente, Acompanhamento, Pedido>(
                sql: commandText,
                map: (pedido, cliente, acompanhamento) =>
                {
                    pedido.Cliente = cliente;
                    pedido.Acompanhamento = acompanhamento;
                    return pedido;
                },
                splitOn: "Id",
                param: new { produtoId });
            return pedidos;
        }
    }
}
