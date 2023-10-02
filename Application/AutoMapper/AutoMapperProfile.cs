﻿using Application.ViewModel;
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



            CreateMap<PedidoViewModel, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCliente));

            CreateMap<PedidoViewModel, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPedido));

            CreateMap<ProdutosPedidoViewModel, ProdutosPedido>();
        }
    }
}
