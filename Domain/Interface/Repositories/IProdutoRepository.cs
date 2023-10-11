using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repositories
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<Produto>> GetAll();
    }
}
