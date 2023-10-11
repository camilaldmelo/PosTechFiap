using Domain.Base;

namespace Domain.ValueObjects
{
    public class Categoria : ValueObject
    {
        private int idCategoria;

        public required int Id { get; set; }
        public required string Nome { get; set; }

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
    }
}
