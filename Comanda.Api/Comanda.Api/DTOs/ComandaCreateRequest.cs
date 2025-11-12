namespace Comanda.Api.DTOs
{
    public class ComandaCreateRequest
    {
        public string NomeCliente { get; set; } = default!;
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int[] CardapioItemsIds { get; set; } = default!;
    }
}
