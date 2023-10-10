using Domain.DTO;
using MyProject.Data.Encryption;
using Npgsql;
using System.Data;

namespace Infra.Repositories
{
    public sealed class RepositoryBase : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public RepositoryBase()
        {
            _id = Guid.NewGuid();
            Connection = ObterConexaoExclusiva();
        }

        public void Dispose() => Connection?.Dispose();
		
        /// <summary>
        /// Faz a conexão com o DB
        /// </summary>
        /// <returns></returns>
        private NpgsqlConnection ObterConexaoExclusiva()
        {
            CriarVariaveisDeAmbiente();

            var senhaCriptografadaBase = Environment.GetEnvironmentVariable("SenhaCriptografadaBase") ?? "";
            var usuarioBase = Environment.GetEnvironmentVariable("UsuarioBase") ?? "";
            var hostBase = Environment.GetEnvironmentVariable("HostBase") ?? "";
            var chaveCriptografiaBase = Environment.GetEnvironmentVariable("ChaveCriptografiaBase") ?? "";

            return ObterConexao(new Conexao(senhaCriptografadaBase, hostBase, usuarioBase, chaveCriptografiaBase, ""));
        }

        /// <summary>
        /// Obtêm a conexão com o DB
        /// </summary>
        /// <param name="conexao"></param>
        /// <returns></returns>
        private static NpgsqlConnection ObterConexao(Conexao conexao)
        {
            var senhaDecripto = AES.Decrypt(conexao.Senha, conexao.ChaveCriptografia);
            conexao.StringConexao = "Host=" + conexao.Instance + ";Username=" + conexao.Usuario + ";Password=" + senhaDecripto + ";";

            var connection = new NpgsqlConnection(conexao.StringConexao);
            connection.Open();
            return connection;
        }

        private static void CriarVariaveisDeAmbiente()
        {
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("SenhaCriptografadaBase")))
                Environment.SetEnvironmentVariable("SenhaCriptografadaBase", "mhoAnVA/tWmZFyrjfl5Qiw==");

            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("UsuarioBase")))
                Environment.SetEnvironmentVariable("UsuarioBase", "postgres");

            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("HostBase")))
                Environment.SetEnvironmentVariable("HostBase", "localhost:5432");

            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ChaveCriptografiaBase")))
                Environment.SetEnvironmentVariable("ChaveCriptografiaBase", "ntmFa1ec294");
        }
    }
}
