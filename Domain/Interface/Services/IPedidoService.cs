using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IPedidoService
    {
        public IEnumerable<Pedido> GetPedido(int idAcompanhamento); 

        public bool PutPedido(Pedido pedido);

        public int PostPedido(Cliente cliente, IEnumerable<ProdutosPedido> produtosPedido);
    }
}
