﻿using Domain.DTO;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class PedidoService : IPedidoService
    {
        public IPedidoRepository _pedidoRepository;
        public IProdutosPedidoRepository _produtosPedidoRepository;
        public IUnitOfWork _unitOfWork;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutosPedidoRepository produtosPedidoRepository, IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _produtosPedidoRepository = produtosPedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Pedido> GetById(int idPedido)
        {
            var pedido = await _pedidoRepository.GetById(idPedido);
            pedido.ProdutosPedido = await _produtosPedidoRepository.GetByIdPedido(pedido.Id);
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> GetByIdStatus(int idAcompanhamento)
        {
            var pedidos = await _pedidoRepository.GetByIdStatus(idAcompanhamento);

            foreach (var pedido in pedidos)
            {
                pedido.ProdutosPedido = await _produtosPedidoRepository.GetByIdPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<int> Create(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido)
        {
            var pedido = new Pedido(idCliente:cliente.Id, data:DateTime.Now, idAcompanhamento:AcompanhamentoConst.Recebido);
            try
            {
                _unitOfWork.BeginTransaction();

                pedido.Id = await _pedidoRepository.Create(pedido);
                foreach (var pp in produtosPedido)
                {
                    pp.IdPedido = pedido.Id;
                    await _produtosPedidoRepository.Create(pp);
                }

                _unitOfWork.Commit();                
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            return pedido.Id;
        }

        public async Task<bool> UpdateStatus(int idPedido, int idStatus)
        {
            try
            {
                var pedido = await _pedidoRepository.GetById(idPedido) ?? throw new Exception("O pedido fornecido não existe.");
                pedido.IdAcompanhamento = idStatus;

                _unitOfWork.BeginTransaction();
                await _pedidoRepository.Update(pedido);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            return true;
        }

        public async Task<bool> Update(Pedido pedido)
        {
            try
            {
                _unitOfWork.BeginTransaction();                
                await _pedidoRepository.Update(pedido);
                await _produtosPedidoRepository.DeleteByIdPedido(pedido.Id);

                if (pedido.ProdutosPedido != null)
                {
                    foreach (var pp in pedido.ProdutosPedido)
                    {
                        pp.IdPedido = pedido.Id;
                        await _produtosPedidoRepository.Create(pp);
                    }
                }
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}
