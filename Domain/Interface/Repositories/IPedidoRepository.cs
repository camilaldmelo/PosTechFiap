using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IPedidoRepository
    {
        public IEnumerable<Pedido> ObterPedidosPorStatusAcompanhamento(int idAcompanhamento);

        public int InserirPedido(Pedido pedido);

        public bool AtualizarPedido(Pedido pedido);
    }
}
