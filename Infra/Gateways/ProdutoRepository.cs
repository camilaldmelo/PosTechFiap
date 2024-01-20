using Dapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Infra.DB;

namespace Infra.Gateways
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
                public.tbl_categoria cat ON cat.id = pro.id_categoria";

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

        public async Task<int> Create(Produto produto)
        {
            string sql = "INSERT INTO public.tbl_produto (nome, preco, descricao, url_imagem, id_categoria) VALUES (@nome, @preco, @descricao, @url_imagem, @id_categoria) RETURNING id";

            var idCategoria = produto.Categoria.Id;
            var parametros = new
            {
                nome = produto.Nome,
                preco = produto.Preco,
                descricao = produto.Descricao,
                url_imagem = produto.UrlImagem,
                id_categoria = idCategoria
            };

            try
            {
                int produtoId = await _session.Connection.ExecuteScalarAsync<int>(sql, parametros);
                return produtoId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            string sql = "DELETE FROM public.tbl_produto WHERE id = @id";

            try
            {
                int rowsAffected = await _session.Connection.ExecuteAsync(sql, new { id });
                return rowsAffected > 0; // Retorna true se uma linha foi afetada (produto excluído).
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetById(int id)
        {
            string sql = commandTextGet + " WHERE pro.id = @id";

            var produto = await _session.Connection.QueryAsync<Produto, Categoria, Produto>(
                sql: sql,
                map: (p, categoria) =>
                {
                    p.Categoria = categoria;
                    return p;
                },
                splitOn: "Id",
                param: new { id }
            );

            return produto.FirstOrDefault(); // Retorna o primeiro produto correspondente ao ID.
        }

        public async Task<bool> Update(Produto produto)
        {
            string sql = @"
                UPDATE public.tbl_produto
                SET nome = @nome,
                    preco = @preco,
                    descricao = @descricao,
                    url_imagem = @url_imagem,
                    id_categoria = @id_categoria
                WHERE id = @id;";

            var parametros = new
            {
                id = produto.Id,
                nome = produto.Nome,
                preco = produto.Preco,
                descricao = produto.Descricao,
                url_imagem = produto.UrlImagem,
                id_categoria = produto.Categoria.Id
            };

            try
            {
                int rowsAffected = await _session.Connection.ExecuteAsync(sql, parametros);

                return rowsAffected > 0; // Retorna true se alguma linha foi afetada (atualização bem-sucedida).
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
