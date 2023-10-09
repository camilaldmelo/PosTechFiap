using Domain.DTO;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infra.Repositories
{
    public class RepositoryBase
    {
        private readonly IConfiguration _config;

        public RepositoryBase(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Faz a conexão com o DB
        /// </summary>
        /// <returns></returns>
        public NpgsqlConnection ObterConexaoExclusiva()
        {
            //var senha = _config.GetValue<string>("ParamRepository:senhaCripto") ?? "";
            //var chave = _config.GetValue<string>("ParamRepository:chave") ?? "";
            //var instance = _config.GetValue<string>("ParamRepository:instance") ?? "";
            //var usuario = _config.GetValue<string>("ParamRepository:usuario") ?? "";
            //var role = _config.GetValue<string>("ParamRepository:role") ?? "";
            //var senhaRoleCripto = _config.GetValue<string>("ParamRepository:senhaRoleCripto") ?? "";

            var senha = "";
            var chave = "";
            var instance = "";
            var usuario = "";
            var role = "";
            var senhaRoleCripto = "";

            return ObterConexao(new Conexao(senha, chave, instance, usuario, role, senhaRoleCripto, "", "", ""));
        }

        /// <summary>
        /// Obtêm a conexão com o DB
        /// </summary>
        /// <param name="conexao"></param>
        /// <returns></returns>
        private NpgsqlConnection ObterConexao(Conexao conexao)
        {
            var senhaDecripto = ""; // DESCRIPTOGRAFIA DE SENHA
            conexao.StringConexao = "Host=" + conexao.Instance + ";Username=" + conexao.Usuario + ";Password=" + senhaDecripto + ";";

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(conexao.StringConexao);
            var dsb = dataSourceBuilder.Build();
            return dsb.OpenConnection();


            // https://www.npgsql.org/doc/basic-usage.html
        }
    }
}
