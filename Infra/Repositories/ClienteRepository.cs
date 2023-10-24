using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.ValueObjects;

namespace Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private RepositoryBase _session;

        public ClienteRepository(RepositoryBase session)
        {
            _session = session;
        }

        /// <summary>
        /// Atualiza pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<int> InserirCliente(Cliente cliente)
        {
            var sql = $@"INSERT INTO public.tbl_cliente (id, nome, cpf, email, ""data"") VALUES(@Id, @Nome, @Cpf, @Email, @Data) RETURNING id;";

            var parametros = new
            {
                cliente.Id,
                cliente.Nome,
                cliente.CPF,
                cliente.Email,
                cliente.Data,
            };

            int clienteId = await _session.Connection.ExecuteScalarAsync<int>(sql, cliente);
            return clienteId;
        }

        public async Task<IEnumerable<Pedido>> ObterClientePorCpf(string cpfCliente)
        {
            string commandText = "SELECT id as Id, nome as Nome, cpf as CPF, email as Email, \"data\" as Data  FROM public.tbl_cliente WHERE cpf = (@cpfCliente)";
            var parametros = new { cpfCliente };

            return await _session.Connection.QueryAsync<Pedido>(commandText, parametros);
        }
    }
}
