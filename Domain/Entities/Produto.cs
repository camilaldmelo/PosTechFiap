namespace Domain.Entities
{
    public class Produto
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public string? UrlImagem { get; set; }
        public required int IdCategoria { get; set; }


    }
}
