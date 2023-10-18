using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.ValueObjects;

namespace Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private RepositoryBase _session;

        private const string commandTextGet  = @"
            SELECT 
                pro.id AS Id,
                pro.nome AS Nome,
                pro.preco AS Preco,
                pro.descricao AS Descricao,
                pro.url_imagem AS UrlImagem,
                cat.id AS Id,
                cat.nome AS Nome
            FROM 
                public.tbl_produto pro
            INNER JOIN 
                tbl_categoria cat ON cat.id = pro.id_categoria";

        public ProdutoRepository(RepositoryBase session)
        {
            _session = session;
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {

            var produtos = await _session.Connection.QueryAsync<Produto, Categoria, Produto>(
                sql: commandTextGet,
                map: (produto, categoria) =>
                {
                    produto.Categoria = categoria; 
                    return produto;
                },
                splitOn: "Id");
            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria)
        {
            string commandTextWhere = @" WHERE cat.id  = (@idCategoria)";

            var produtos = await _session.Connection.QueryAsync<Produto, Categoria, Produto>(
                sql: commandTextGet + commandTextWhere,
                map: (produto, categoria) =>
                {
                    produto.Categoria = categoria;
                    return produto;
                },
                splitOn: "Id",
                param : new { idCategoria });
            return produtos;
        }

        public async Task<int> GetLastID()
        {
            string commandText = "SELECT id FROM public.tbl_produto ORDER BY id DESC LIMIT 1";
            return await _session.Connection.QueryFirstOrDefaultAsync<int>(commandText);
        }

        public async Task<bool> Post(Produto produto)
        {
            string sql = "INSERT INTO public.tbl_produto (id, nome, preco, descricao, url_imagem, id_categoria) VALUES (@id, @nome, @preco, @descricao, @url_imagem, @id_categoria);";

            var idCategoria = produto.Categoria.Id;
            var parametros = new
            {
                id = produto.Id,
                nome = produto.Nome,
                preco = produto.Preco,
                descricao = produto.Descricao,
                url_imagem = produto.UrlImagem,
                id_categoria = idCategoria
            };

            try
            {
                await _session.Connection.ExecuteAsync(sql, parametros);
                return true;
            }
            catch (Exception e )
            {
                throw new Exception(e.Message);
            }
        }
    }
}
