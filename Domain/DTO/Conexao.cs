using System.Diagnostics.CodeAnalysis;

namespace Domain.DTO
{
    public class Conexao
    {
        [SetsRequiredMembers]
        public Conexao(string senha, string instance, string usuario, string stringConexao) => 
            (Senha, Instance, Usuario, StringConexao) = (senha, instance, usuario, stringConexao);

        public required string Senha { get; set; }
        public required string Instance { get; set; }
        public required string Usuario { get; set; }
        public required string StringConexao { get; set; }
        
    }
}
