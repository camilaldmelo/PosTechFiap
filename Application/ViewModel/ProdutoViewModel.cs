namespace Application.ViewModel
{
    public class ProdutoViewModel
    {
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public required string Categoria { get; set; }
        public decimal Preco { get; set; }        
        public string? UrlImagem { get; set; }        
    }
}