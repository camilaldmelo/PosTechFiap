using Application.ViewModel;

namespace Application.Interface
{
    public interface IProdutoUseCase
    {
        public Task<IEnumerable<ProdutoViewModel>> GetAll();
    }
}
