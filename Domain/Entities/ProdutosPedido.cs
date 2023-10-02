namespace Domain.Entities
{
    public class ProdutosPedido
    {
        public required int Id { get; set; }
        public required int IdPedido { get; set; }
        public required int IdProduto { get; set; }
        public required int Quantidade { get; set; }
    }
}
