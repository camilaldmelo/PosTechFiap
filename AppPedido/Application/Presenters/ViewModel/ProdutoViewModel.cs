namespace Application.Presenters.ViewModel
{
    public class ProdutoViewModel
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string UrlImagem { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
    }
}