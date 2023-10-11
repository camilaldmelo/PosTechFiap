using Domain.Base;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Produto : IAggregateRoot
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public string? UrlImagem { get; set; }
        public Categoria Categoria { get; set; }

        public Produto(int id, string nome, decimal preco, string descricao, string urlImagem)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            UrlImagem = urlImagem;

            ValidateEntity();
        }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome do produto não pode ser vazio.");
            if (Preco <= 0)
                throw new DomainException("O preço do produto deve ser maior que zero.");
        }

    }
}
