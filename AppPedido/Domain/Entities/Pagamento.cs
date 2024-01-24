using Domain.Base;

namespace Domain.Entities
{
    public class Pagamento : IAggregateRoot
    {
        public Pagamento() { }

        public int Id { get; set; }
        public double Valor { get; set; }
        public int IdPedido { get; set; }
        public int IdMetodoPagamento { get; set; }
        public virtual Pedido Pedido { get; set; } = null!;
    }
}
