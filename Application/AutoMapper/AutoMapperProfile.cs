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
                .ForMember(dest => dest.Cliente.IdCliente, opt => opt.MapFrom(src => src.Id));

            CreateMap<Pedido, PedidoViewModel>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.Id));

            CreateMap<ProdutosPedido, ProdutosPedidoViewModel>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome));



            CreateMap<PedidoViewModel, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Cliente.IdCliente));

            CreateMap<PedidoViewModel, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPedido));

            CreateMap<ProdutosPedidoViewModel, ProdutosPedido>();

            CreateMap<ClienteViewModel, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCliente));

        }
    }
}
