using Application.Interface.Presenters;
using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;

namespace Application.Presenters
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        public ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaUseCase(IMapper mapper, ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetAll()
        {
            var categorias = await _categoriaService.GetAll();

            return _mapper.Map<List<CategoriaViewModel>>(categorias);
        }

        public async Task<CategoriaViewModel> GetById(int id)
        {
            var categoria = await _categoriaService.GetById(id);

            return _mapper.Map<CategoriaViewModel>(categoria);
        }

        public async Task<int> Create(CategoriaViewModel categoria)
        {
            var c = _mapper.Map<Categoria>(categoria);
            try
            {
                return await _categoriaService.Create(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, CategoriaViewModel categoria)
        {
            var existingCategoria = await _categoriaService.GetById(id);

            if (existingCategoria == null)
            {
                return false; // Categoria não encontrada, portanto, não foi atualizada.
            }

            // Realize as validações necessárias no objeto 'categoria' e manipule exceções, se necessário.

            // Atualize as propriedades da categoria existente com base nos dados de 'categoria'.
            existingCategoria.Nome = categoria.Nome;

            return await _categoriaService.Update(existingCategoria); // Atualiza a categoria no serviço.
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _categoriaService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
