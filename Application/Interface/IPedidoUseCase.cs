using Application.ViewModel;

namespace Application.Interface
{
    public interface IPedidoUseCase
    {
        public IEnumerable<PedidoViewModel> GetPedido(int idAcompanhamento);

        public bool PutPedido(PedidoViewModel pedido);

        public int PostPedido(PedidoViewModel pedido);
    }
}
