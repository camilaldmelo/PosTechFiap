using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.ValueObjects;

namespace Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private RepositoryBase _session;

        public ProdutoRepository(RepositoryBase session)
        {
            _session = session;
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            string commandText = @"
                SELECT 
                    pro.id AS Id,
                    pro.nome AS Nome,
                    pro.preco AS Preco,
                    pro.descricao AS Descricao,
                    pro.url_imagem AS UrlImagem,
                    cat.id AS Id,
                    cat.nome AS Nome
                FROM 
                    tbl_produto pro
                INNER JOIN 
                    tbl_categoria cat ON cat.id = pro.id_categoria";

            var produtos = await _session.Connection.QueryAsync<Produto, Categoria, Produto>(
                sql: commandText,
                map: (produto, categoria) =>
                {
                    produto.Categoria = categoria; 
                    return produto;
                },
                splitOn: "Id");
            return produtos;
        }
    }
}
