using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface IProdutoService
    {
        public Task<IEnumerable<Produto>> GetAll();
        public Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria);
        public Task<int> Create(Produto produto);
        public Task<bool> Delete(int id);
        public Task<Produto> GetById(int id);
        public Task<bool> Update(Produto produto);
    }
}
