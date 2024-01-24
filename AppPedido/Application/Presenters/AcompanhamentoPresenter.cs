using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;
using Application.Interface.UseCases;

namespace Application.Presenters
{
    public class AcompanhamentoPresenter : IAcompanhamentoPresenter
    {
        private readonly IAcompanhamentoUseCase _acompanhamentoUseCase;
        private readonly IMapper _mapper;

        public AcompanhamentoPresenter(IMapper mapper, IAcompanhamentoUseCase acompanhamentoUseCase)
        {
            _acompanhamentoUseCase = acompanhamentoUseCase;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AcompanhamentoViewModel>> GetAll()
        {
            var acompanhamentos = await _acompanhamentoUseCase.GetAll();
            return _mapper.Map<List<AcompanhamentoViewModel>>(acompanhamentos);
        }

        public async Task<AcompanhamentoViewModel> GetById(int id)
        {
            var acompanhamento = await _acompanhamentoUseCase.GetById(id);
            return _mapper.Map<AcompanhamentoViewModel>(acompanhamento);
        }

        public async Task<int> Create(AcompanhamentoViewModel acompanhamento)
        {
            var a = _mapper.Map<Acompanhamento>(acompanhamento);
            try
            {
                return await _acompanhamentoUseCase.Create(a);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, AcompanhamentoViewModel acompanhamento)
        {
            var existingAcompanhamento = await _acompanhamentoUseCase.GetById(id);

            if (existingAcompanhamento == null)
            {
                return false; // Acompanhamento não encontrado, portanto, não foi atualizado.
            }

            // Realize as validações necessárias no objeto 'acompanhamento' e manipule exceções, se necessário.

            // Atualize as propriedades do acompanhamento existente com base nos dados de 'acompanhamento'.
            existingAcompanhamento.Nome = acompanhamento.Nome;

            return await _acompanhamentoUseCase.Update(existingAcompanhamento); // Atualiza o acompanhamento no serviço.
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _acompanhamentoUseCase.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
