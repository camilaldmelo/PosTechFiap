using Domain.Base;

namespace Domain.Entities
{
    public class Pedido : IAggregateRoot
    {
        public Pedido () { }
        public Pedido(int idCliente, DateTime data, int idAcompanhamento, IEnumerable<ProdutosPedido>? produtosPedido = null, Cliente? cliente = null, Acompanhamento? acompanhamento = null)
        {
            this.IdCliente = idCliente;
            this.Data = data;
            this.IdAcompanhamento = idAcompanhamento;

            this.Acompanhamento = acompanhamento;
            this.Cliente = cliente;
            this.ProdutosPedido = produtosPedido;

            ValidateEntity();
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Data { get; set; }
        public int IdAcompanhamento { get; set; }


        public Acompanhamento? Acompanhamento { get; set; }
        public Cliente? Cliente { get; set; }
        public IEnumerable<ProdutosPedido>? ProdutosPedido { get; set; }


        public void ValidateEntity()
        {
            if (IdCliente < 0)
                throw new DomainException("O IdCliente precisa ser maior ou igual a zero.");
        }
    }
}
