namespace Domain.Entities
{
    public class Pedido
    {
        public Pedido () { }
        public Pedido (int id, int idCliente, DateTime data, int idAcompanhamento, IEnumerable<ProdutosPedido>? produtosPedido) =>
                      (Id, IdCliente, Data, IdAcompanhamento, ProdutosPedido) = (id, idCliente, data, idAcompanhamento, produtosPedido);

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Data { get; set; }
        public int IdAcompanhamento { get; set; }

        public IEnumerable<ProdutosPedido>? ProdutosPedido { get; set; }
    }
}
