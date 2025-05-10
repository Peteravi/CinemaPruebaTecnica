namespace Cinema.Application.DTOs
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public ClienteDto Cliente { get; set; } = null!;
        public CarteleraDto Cartelera { get; set; } = null!;
        public ButacaDto Butaca { get; set; } = null!;
        public DateTime FechaReserva { get; set; }
        public string Estado { get; set; } = "activa";
    }
}
