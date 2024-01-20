using Dapper;
using Domain.Entities;
using Domain.Interface.Gateways;
using Infra.DB;

namespace Infra.Gateways
{
    public class ClienteGateways : IClienteGateways
    {
        private RepositoryDB _session;

        public ClienteGateways(RepositoryDB session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            string sql = "SELECT id, nome, cpf, email, data FROM public.tbl_cliente";

            var clientes = await _session.Connection.QueryAsync<Cliente>(sql);
            return clientes;
        }

        public async Task<Cliente> GetById(int id)
        {
            string sql = "SELECT id, nome, cpf, email, data FROM public.tbl_cliente WHERE id = @id";

            var cliente = await _session.Connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { id });
            return cliente;
        }
        public async Task<Cliente> GetByCPF(string cpf)
        {
            string sql = "SELECT id, nome, cpf, email, data FROM public.tbl_cliente WHERE cpf = @cpf";

            var cliente = await _session.Connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { cpf });
            return cliente;
        }

        public async Task<int> Create(Cliente cliente)
        {
            string sql = "INSERT INTO public.tbl_cliente (nome, cpf, email, data) VALUES (@nome, @cpf, @email, @data) RETURNING id";

            try
            {
                int clienteId = await _session.Connection.ExecuteScalarAsync<int>(sql, cliente);
                return clienteId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Cliente cliente)
        {
            string sql = "UPDATE public.tbl_cliente SET nome = @nome, cpf = @cpf, email = @email, data = @data WHERE id = @id";

            try
            {
                int rowsAffected = await _session.Connection.ExecuteAsync(sql, cliente);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            string sql = "DELETE FROM public.tbl_cliente WHERE id = @id";
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
