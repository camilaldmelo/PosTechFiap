using Application.Presenters.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, PedidoIncViewModel>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Id));

            CreateMap<Pedido, PedidoIncViewModel>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.Id));

            CreateMap<Pedido, PedidoViewModel>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.Id));

            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Id));

            CreateMap<ProdutosPedido, ProdutosPedidoIncViewModel>();

            CreateMap<ProdutosPedido, ProdutosPedidoViewModel>();

            CreateMap<ProdutosPedido, ProdutoViewModel>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.IdProduto, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Categoria.Id))
                .ForPath(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome));

            CreateMap<Categoria, CategoriaViewModel>()
                .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Id));

            CreateMap<Acompanhamento, AcompanhamentoViewModel>()
                .ForMember(dest => dest.IdAcompanhamento, opt => opt.MapFrom(src => src.Id));

            CreateMap<PedidoIncViewModel, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCliente));

            CreateMap<PedidoIncViewModel, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPedido));

            CreateMap<PedidoViewModel, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCliente));

            CreateMap<PedidoViewModel, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPedido));

            CreateMap<ProdutosPedidoIncViewModel, ProdutosPedido>();

            CreateMap<ProdutosPedidoViewModel, ProdutosPedido>();

            CreateMap<ProdutoViewModel, ProdutosPedido>();

            CreateMap<ProdutoViewModel, Produto>()
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProduto))
                      .ForPath(dest => dest.Categoria.Id, opt => opt.MapFrom(src => src.IdCategoria))
                      .ForPath(dest => dest.Categoria.Nome, opt => opt.MapFrom(src => src.Categoria));

            CreateMap<CategoriaViewModel, Categoria>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCategoria));

            CreateMap<AcompanhamentoViewModel, Acompanhamento>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdAcompanhamento));

            CreateMap<ClienteViewModel, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCliente));


        }
    }
}
