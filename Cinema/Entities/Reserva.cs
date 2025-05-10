namespace Cinema.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public int CarteleraId { get; set; }
        public Cartelera Cartelera { get; set; } = null!;
        public int ButacaId { get; set; }
        public Butaca Butaca { get; set; } = null!;
        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "activa"; // activa o cancelada
    }

}
