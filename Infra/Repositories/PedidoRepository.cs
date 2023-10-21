using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;

namespace Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private RepositoryBase _session;

        public PedidoRepository(RepositoryBase session)
        {
            _session = session;
        }

        /// <summary>
        /// Atualiza pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
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

            var ret = await _session.Connection.ExecuteAsync(sql, parameters);
            return ret > 0;
        }

        /// <summary>
        /// Insere pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<bool> Create(Pedido pedido)
        {
            string sql = "INSERT INTO public.tbl_pedido (id, id_cliente, \"data\", id_acompanhamento) VALUES(@Id, @IdCliente, @Data, @IdAcompanhamento);";

            var parameters = new
            {
                pedido.Id,
                pedido.IdCliente,
                pedido.Data,
                pedido.IdAcompanhamento
            };

            await _session.Connection.ExecuteAsync(sql, parameters, _session.Transaction);
            return true;
        }

        public async Task<int> GetIdLastRecordInserted()
        {
            //string commandText = "SELECT currval(pg_get_serial_sequence('public.tbl_pedido','id'))";
            string commandText = "SELECT id FROM public.tbl_pedido ORDER BY id DESC LIMIT 1";
            return await _session.Connection.QueryFirstOrDefaultAsync<int>(commandText);
        }

        public async Task<IEnumerable<Pedido>> GetById(int idPedido)
        {
            string commandText = "SELECT id as Id, id_cliente as IdCliente, \"data\" as Data, id_acompanhamento as IdAcompanhamento FROM public.tbl_pedido WHERE id = (@idPedido)";
            var parameters = new { idPedido };

            return await _session.Connection.QueryAsync<Pedido>(commandText, parameters);
        }
    }
}
