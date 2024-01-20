using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;
using Application.Interface.UseCases;

namespace Application.Presenters
{
    public class AcompanhamentoPresenters : IAcompanhamentoPresenters
    {
        private readonly IAcompanhamentoUseCases _acompanhamentoService;
        private readonly IMapper _mapper;

        public AcompanhamentoPresenters(IMapper mapper, IAcompanhamentoUseCases acompanhamentoService)
        {
            _acompanhamentoService = acompanhamentoService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AcompanhamentoViewModel>> GetAll()
        {
            var acompanhamentos = await _acompanhamentoService.GetAll();
            return _mapper.Map<List<AcompanhamentoViewModel>>(acompanhamentos);
        }

        public async Task<AcompanhamentoViewModel> GetById(int id)
        {
            var acompanhamento = await _acompanhamentoService.GetById(id);
            return _mapper.Map<AcompanhamentoViewModel>(acompanhamento);
        }

        public async Task<int> Create(AcompanhamentoViewModel acompanhamento)
        {
            var a = _mapper.Map<Acompanhamento>(acompanhamento);
            try
            {
                return await _acompanhamentoService.Create(a);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, AcompanhamentoViewModel acompanhamento)
        {
            var existingAcompanhamento = await _acompanhamentoService.GetById(id);

            if (existingAcompanhamento == null)
            {
                return false; // Acompanhamento não encontrado, portanto, não foi atualizado.
            }

            // Realize as validações necessárias no objeto 'acompanhamento' e manipule exceções, se necessário.

            // Atualize as propriedades do acompanhamento existente com base nos dados de 'acompanhamento'.
            existingAcompanhamento.Nome = acompanhamento.Nome;

            return await _acompanhamentoService.Update(existingAcompanhamento); // Atualiza o acompanhamento no serviço.
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _acompanhamentoService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
