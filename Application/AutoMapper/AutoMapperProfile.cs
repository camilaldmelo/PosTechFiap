using Application.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, PedidoViewModel>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Id));

            CreateMap<Pedido, PedidoViewModel>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.Id));

            CreateMap<ProdutosPedido, ProdutosPedidoViewModel>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.IdProduto, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Categoria.Id))
                .ForPath(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome));

            CreateMap<Categoria, CategoriaViewModel>()
                .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Id));

            CreateMap<Acompanhamento, AcompanhamentoViewModel>()
                .ForMember(dest => dest.IdAcompanhamento, opt => opt.MapFrom(src => src.Id));



            CreateMap<PedidoViewModel, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCliente));

            CreateMap<PedidoViewModel, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPedido));

            CreateMap<ProdutosPedidoViewModel, ProdutosPedido>();

            CreateMap<ProdutoViewModel, Produto>()
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProduto))
                      .ForPath(dest => dest.Categoria.Id, opt => opt.MapFrom(src => src.IdCategoria))
                      .ForPath(dest => dest.Categoria.Nome, opt => opt.MapFrom(src => src.Categoria));

            CreateMap<CategoriaViewModel, Categoria>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCategoria));

            CreateMap<AcompanhamentoViewModel, Acompanhamento>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdAcompanhamento));

        }
    }
}
