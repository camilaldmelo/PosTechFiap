using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IAcompanhamentoService
    {
        public Task<IEnumerable<Acompanhamento>> GetAll();
        public Task<Acompanhamento> GetById(int id);
        public Task<int> Create(Acompanhamento acompanhamento);
        public Task<bool> Update(Acompanhamento acompanhamento);
        public Task<bool> Delete(int id);
    }
}
