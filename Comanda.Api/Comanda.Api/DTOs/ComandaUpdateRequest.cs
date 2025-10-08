namespace Comanda.Api.DTOs
{
    public class ComandaUpdateRequest
    {
        public string NomeCliente { get; set; } = default!;
        public int NumeroMesa { get; set; }
        public int[] CardapioItemsIds { get; set; } = default!;
    }
}
