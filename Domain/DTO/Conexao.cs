using System.Diagnostics.CodeAnalysis;

namespace Domain.DTO
{
    public class Conexao
    {
        [SetsRequiredMembers]
        public Conexao(string senha, string chave, string instance, string usuario, string? role, string? senhaRoleCripto, string owner, string stringConexao, string? comandoInicial) =>
                       (Senha, Chave, Instance, Usuario, Role, SenhaRoleCripto, Owner, StringConexao, ComandoInicial) =
                       (senha, chave, instance, usuario, role, senhaRoleCripto, owner, stringConexao, comandoInicial);

        public required string Senha { get; set; }
        public required string Chave { get; set; }
        public required string Instance { get; set; }
        public required string Usuario { get; set; }
        public string? Role { get; set; }
        public string? SenhaRoleCripto { get; set; }
        public required string Owner { get; set; }
        public required string StringConexao { get; set; }
        public string? ComandoInicial { get; set; }
    }
}
