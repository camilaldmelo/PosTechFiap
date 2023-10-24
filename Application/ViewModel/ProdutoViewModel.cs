namespace Application.ViewModel
{
    public class ProdutoViewModel
    {
        public required int IdProduto { get; set; }
        public required string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }        
        public string UrlImagem { get; set; }
        public required int IdCategoria { get; set; }
        public required string Categoria { get; set; }
    }
}