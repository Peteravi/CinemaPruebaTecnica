namespace Cinema.Application.DTOs
{
    public class CarteleraDto
    {
        public int Id { get; set; }
        public PeliculaDto Pelicula { get; set; } = null!;
        public SalaDto Sala { get; set; } = null!;
        public DateTime FechaFuncion { get; set; }
        public TimeSpan HoraFuncion { get; set; }
        public bool Estado { get; set; }
    }
}
