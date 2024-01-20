using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Infra.DB;

namespace Infra.Gateways
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private RepositoryBase _session;

        public PagamentoRepository(RepositoryBase session)
        {
            _session = session;
        }

        public async Task<int> Create(Pagamento pagamento)
        {
            string sql = "INSERT INTO public.tbl_pagamento (id_pedido, id_metodo, valor, data_criacao, data_atualizacao) VALUES (:id_pedido, :id_metodo, :valor, :data_criacao, :data_atualizacao) RETURNING id";

            try
            {
                var parameters = new
                {
                    id_pedido = pagamento.IdPedido,
                    id_metodo = pagamento.IdMetodoPagamento,
                    valor = pagamento.Valor,
                    data_criacao = DateTime.Now,
                    data_atualizacao = DateTime.Now
                };

                int pagamentoId = await _session.Connection.ExecuteScalarAsync<int>(sql, parameters);
                return pagamentoId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pagamento?> GetByIdPedido(int idPedido)
        {
            string sql = @" SELECT pg.id, 
                                   pg.valor,                                    
                                   pg.id_pedido,
                                   pg.id_metodo
                              FROM public.tbl_pagamento pg INNER JOIN
                                   public.tbl_pedido pd ON pd.id = pg.id_pedido
                             WHERE id_pedido = @idPedido";

            var pagamento = await _session.Connection.QueryFirstOrDefaultAsync<Pagamento>(sql, new { idPedido });
            return pagamento;
        }

    }
}
