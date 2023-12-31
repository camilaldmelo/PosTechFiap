﻿using Application.ViewModel;


namespace Application.Interface
{
    public interface IAcompanhamentoUseCase
    {
        public Task<IEnumerable<AcompanhamentoViewModel>> GetAll();
        public Task<AcompanhamentoViewModel> GetById(int id);
        public Task<int> Create(AcompanhamentoViewModel acompanhamento);
        public Task<bool> Update(int id, AcompanhamentoViewModel acompanhamento);
        public Task<bool> Delete(int id);
    }
}
