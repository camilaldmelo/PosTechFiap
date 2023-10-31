using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IAcompanhamentoRepository
    {
        public Task<IEnumerable<Acompanhamento>> GetAll();
        public Task<Acompanhamento> GetById(int id);
        public Task<int> Create(Acompanhamento acompanhamento);
        public Task<bool> Update(Acompanhamento acompanhamento);
        public Task<bool> Delete(int id);
    }
}
