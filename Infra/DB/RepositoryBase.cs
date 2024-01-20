using Domain.DTO;
using Npgsql;
using System.Data;

namespace Infra.DB
{
    public sealed class RepositoryBase : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public RepositoryBase()
        {
            _id = Guid.NewGuid();
            Connection = GetExclusiveConnection();
        }

        public void Dispose() => Connection?.Dispose();

        /// <summary>
        /// Faz a conexão com o DB
        /// </summary>
        /// <returns></returns>
        private NpgsqlConnection GetExclusiveConnection()
        {
            CreateEnvironmentVariables();

            var SenhaBase = Environment.GetEnvironmentVariable("SenhaBase") ?? "";
            var userBase = Environment.GetEnvironmentVariable("UsuarioBase") ?? "";
            var hostBase = Environment.GetEnvironmentVariable("HostBase") ?? "";

            return GetConnection(new Conexao(SenhaBase, hostBase, userBase, ""));
        }

        /// <summary>
        /// Obtêm a conexão com o DB
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static NpgsqlConnection GetConnection(Conexao connection)
        {
            connection.StringConexao = "Host=" + connection.Instance + ";Username=" + connection.Usuario + ";Password=" + connection.Senha + ";";

            var npgConnection = new NpgsqlConnection(connection.StringConexao);
            npgConnection.Open();
            return npgConnection;
        }

        private static void CreateEnvironmentVariables()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SenhaBase")))
                Environment.SetEnvironmentVariable("SenhaBase", "123456");

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("UsuarioBase")))
                Environment.SetEnvironmentVariable("UsuarioBase", "postgres");

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("HostBase")))
                Environment.SetEnvironmentVariable("HostBase", "localhost:5432");
        }
    }
}
