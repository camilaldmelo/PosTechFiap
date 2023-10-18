using System.Collections.Generic;
using Domain.Base;

namespace Domain.ValueObjects
{
    public class Categoria : ValueObject
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        private Categoria() { } // Construtor privado para uso pelo Entity Framework

        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Nome;
        }

        // Métodos de fábrica para criar instâncias de Categoria
        public static Categoria CriarNova(int id, string nome)
        {
            return new Categoria(id, nome);
        }
    }
}
