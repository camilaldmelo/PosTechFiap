using Domain.Entities;

namespace Application.Interface.UseCases
{
    public interface IAcompanhamentoUseCase
    {
        public Task<IEnumerable<Acompanhamento>> GetAll();
        public Task<Acompanhamento> GetById(int id);
        public Task<int> Create(Acompanhamento acompanhamento);
        public Task<bool> Update(Acompanhamento acompanhamento, int id);
        public Task<bool> Delete(int id);
    }
}
