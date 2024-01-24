using Dapper;
using Domain.Entities;
using Domain.Interface.Gateways;
using Infra.DB;

namespace Infra.Gateways
{
    public class CategoriaGateway : ICategoriaGateway
    {
        private RepositoryDB _session;

        public CategoriaGateway(RepositoryDB session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            string sql = "SELECT id, nome FROM public.tbl_categoria";

            var categorias = await _session.Connection.QueryAsync<Categoria>(sql);
            return categorias;
        }

        public async Task<Categoria> GetById(int id)
        {
            string sql = "SELECT id, nome FROM public.tbl_categoria WHERE id = @id";

            var categoria = await _session.Connection.QueryFirstOrDefaultAsync<Categoria>(sql, new { id });
            return categoria;
        }

        public async Task<int> Create(Categoria categoria)
        {
            string sql = "INSERT INTO public.tbl_categoria (nome) VALUES (@nome) RETURNING id";

            try
            {
                int categoriaId = await _session.Connection.ExecuteScalarAsync<int>(sql, categoria);
                return categoriaId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Categoria categoria)
        {
            string sql = "UPDATE public.tbl_categoria SET nome = @nome WHERE id = @id";

            try
            {
                int rowsAffected = await _session.Connection.ExecuteAsync(sql, categoria);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            string sql = "DELETE FROM public.tbl_categoria WHERE id = @id";

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
