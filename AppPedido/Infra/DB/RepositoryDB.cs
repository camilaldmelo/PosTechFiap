using Domain.DTO;
using Npgsql;
using System.Data;

namespace Infra.DB
{
    public sealed class RepositoryDB : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public RepositoryDB()
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

            var SenhaBase = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
            var userBase = Environment.GetEnvironmentVariable("DB_USER") ?? "";
            var hostBase = Environment.GetEnvironmentVariable("DB_HOST") ?? "";

            return GetConnection(new Conexao(SenhaBase, hostBase, userBase));
        }

        /// <summary>
        /// Obtêm a conexão com o DB
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static NpgsqlConnection GetConnection(Conexao connection)
        {
            var npgConnection = new NpgsqlConnection("Host=" + connection.Instance + ";Username=" + connection.Usuario + ";Password=" + connection.Senha + ";");
            npgConnection.Open();
            return npgConnection;
        }

        private static void CreateEnvironmentVariables()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_PASSWORD")))
                Environment.SetEnvironmentVariable("DB_PASSWORD", "123456");

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_USER")))
                Environment.SetEnvironmentVariable("DB_USER", "postgres");

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_HOST")))
                Environment.SetEnvironmentVariable("DB_HOST", "localhost:5432");
        }
    }
}
