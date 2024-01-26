using Domain.Base;

namespace Domain.Entities
{
    public class ProdutosPedido : IAggregateRoot
    {
        public ProdutosPedido() { }

        public required int Id { get; set; }
        public required int IdPedido { get; set; }
        public required int IdProduto { get; set; }
        public required int Quantidade { get; set; }

        public Produto Produto { get; set; }
        
        public void ValidateEntity()
        {
            if (Quantidade <= 0)
                throw new DomainException("A quantidade do produto não pode ser zero.");
        }
    }
}
