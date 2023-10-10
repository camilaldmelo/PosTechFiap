using System.Diagnostics.CodeAnalysis;

namespace Domain.DTO
{
    public class Conexao
    {
        [SetsRequiredMembers]
        public Conexao(string senha, string instance, string usuario, string chaveCriptografia, string stringConexao) => 
            (Senha, Instance, Usuario, ChaveCriptografia, StringConexao) = (senha, instance, usuario, chaveCriptografia, stringConexao);

        public required string Senha { get; set; }
        public required string Instance { get; set; }
        public required string Usuario { get; set; }
        public required string ChaveCriptografia { get; set; }
        public required string StringConexao { get; set; }
        
    }
}
