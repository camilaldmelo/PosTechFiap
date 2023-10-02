namespace Domain.Entities
{
    public class Pedido
    {
        public Pedido () { }
        public Pedido (int idCliente, DateTime data, int idAcompanhamento, IEnumerable<ProdutosPedido>? produtosPedido) =>
                      (IdCliente, Data, IdAcompanhamento, ProdutosPedido) = (idCliente, data, idAcompanhamento, produtosPedido);

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Data { get; set; }
        public int IdAcompanhamento { get; set; }

        public IEnumerable<ProdutosPedido>? ProdutosPedido { get; set; }
    }
}
