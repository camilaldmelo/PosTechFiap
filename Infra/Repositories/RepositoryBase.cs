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

            var passwordEncryptedBase = Environment.GetEnvironmentVariable("SenhaCriptografadaBase") ?? "";
            var userBase = Environment.GetEnvironmentVariable("UsuarioBase") ?? "";
            var hostBase = Environment.GetEnvironmentVariable("HostBase") ?? "";
            var keyEncryptionBase = Environment.GetEnvironmentVariable("ChaveCriptografiaBase") ?? "";

            return GetConnection(new Conexao(passwordEncryptedBase, hostBase, userBase, keyEncryptionBase, ""));
        }

        /// <summary>
        /// Obtêm a conexão com o DB
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static NpgsqlConnection GetConnection(Conexao connection)
        {
            var passwordDecrypt = AES.Decrypt(connection.Senha, connection.ChaveCriptografia);
            connection.StringConexao = "Host=" + connection.Instance + ";Username=" + connection.Usuario + ";Password=" + passwordDecrypt + ";";

            var npgConnection = new NpgsqlConnection(connection.StringConexao);
            npgConnection.Open();
            return npgConnection;
        }

        private static void CreateEnvironmentVariables()
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
