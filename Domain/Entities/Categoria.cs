
using Domain.Base;

namespace Domain.Entities
{
    public class Categoria : IAggregateRoot
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        private Categoria() { } // Construtor privado para uso pelo Dapper

        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
