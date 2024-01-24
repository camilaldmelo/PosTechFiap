using Domain.Base;

namespace Domain.Entities
{
    public class Acompanhamento : IAggregateRoot
    {
        public Acompanhamento() { }

        public required int Id { get; set; }
        public required string Nome { get; set; }
    }
}
