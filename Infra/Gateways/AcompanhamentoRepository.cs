using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Infra.DB;

namespace Infra.Gateways
{
    public class AcompanhamentoRepository : IAcompanhamentoRepository
    {
        private RepositoryBase _session;

        public AcompanhamentoRepository(RepositoryBase session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Acompanhamento>> GetAll()
        {
            string sql = "SELECT id, nome FROM public.tbl_acompanhamento";

            var acompanhamentos = await _session.Connection.QueryAsync<Acompanhamento>(sql);
            return acompanhamentos;
        }

        public async Task<Acompanhamento> GetById(int id)
        {
            string sql = "SELECT id, nome FROM public.tbl_acompanhamento WHERE id = @id";

            var acompanhamento = await _session.Connection.QueryFirstOrDefaultAsync<Acompanhamento>(sql, new { id });
            return acompanhamento;
        }

        public async Task<int> Create(Acompanhamento acompanhamento)
        {
            string sql = "INSERT INTO public.tbl_acompanhamento (nome) VALUES (@nome) RETURNING id";

            try
            {
                int acompanhamentoId = await _session.Connection.ExecuteScalarAsync<int>(sql, acompanhamento);
                return acompanhamentoId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Acompanhamento acompanhamento)
        {
            string sql = "UPDATE public.tbl_acompanhamento SET nome = @nome WHERE id = @id";

            try
            {
                int rowsAffected = await _session.Connection.ExecuteAsync(sql, acompanhamento);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            string sql = "DELETE FROM public.tbl_acompanhamento WHERE id = @id";

            try
            {
                int rowsAffected = await _session.Connection.ExecuteAsync(sql, new { id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
