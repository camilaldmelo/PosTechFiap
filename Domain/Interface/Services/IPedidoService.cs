﻿using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IPedidoService
    {
        public Task<Pedido> GetById(int idPedido);
        public Task<IEnumerable<Pedido>> GetByIdStatus(int idAcompanhamento);
        public Task<IEnumerable<Pedido>> GetByIdCliente(int idCliente);
        public Task<IEnumerable<Pedido>> GetByIdProduto(int produtoId);
        public Task<bool> UpdateStatus(int idPedido, int idStatus);
        public Task<bool> Update(Pedido pedido);
        public Task<int> Create(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido);
        
    }
}
