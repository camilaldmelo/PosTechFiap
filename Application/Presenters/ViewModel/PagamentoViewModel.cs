namespace Application.Presenters.ViewModel
{
    public class PagamentoViewModel
    {
        public required int IdPedido { get; set; }
        public required bool Aprovado { get; set; }
        public string? Motivo { get; set; }
    }
}
