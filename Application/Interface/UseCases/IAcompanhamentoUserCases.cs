using Domain.Entities;

namespace Application.Interface.UserCases
{
    public interface IAcompanhamentoUserCases
    {
        public Task<IEnumerable<Acompanhamento>> GetAll();
        public Task<Acompanhamento> GetById(int id);
        public Task<int> Create(Acompanhamento acompanhamento);
        public Task<bool> Update(Acompanhamento acompanhamento);
        public Task<bool> Delete(int id);
    }
}
