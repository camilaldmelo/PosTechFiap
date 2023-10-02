using Domain.Entities;
using Domain.Interface.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infra.Repositories
{
    public class PedidoRepository : RepositoryBase, IPedidoRepository
    {
        public PedidoRepository(IConfiguration config) : base(config) { }

        public bool AtualizarPedido(Pedido pedido)
        {
            return true;
        }

        public int InserirPedido(Pedido pedido)
        {
            return 77;
        }

        public IEnumerable<Pedido> ObterPedidosPorStatusAcompanhamento(int idAcompanhamento)
        {
            return new List<Pedido>();
        }
    }
}
