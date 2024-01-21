using System.Diagnostics.CodeAnalysis;

namespace Domain.DTO
{
    public class Conexao
    {
        [SetsRequiredMembers]
        public Conexao(string senha, string instance, string usuario) => 
            (Senha, Instance, Usuario) = (senha, instance, usuario);

        public required string Senha { get; set; }
        public required string Instance { get; set; }
        public required string Usuario { get; set; }        
    }
}
