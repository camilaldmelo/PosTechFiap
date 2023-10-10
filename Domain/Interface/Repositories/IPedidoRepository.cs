using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IPedidoRepository
    {
        public Task<IEnumerable<Pedido>> ObterPedidosPorId(int idPedido);

        public Task<bool> InserirPedido(Pedido pedido);

        public Task<int> ObterIdUltimoRegistroInserido();

        public Task<bool> AtualizarPedido(Pedido pedido);
    }
}
