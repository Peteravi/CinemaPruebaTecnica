namespace Cinema.Entities
{
    public class Cartelera
    {
        public int Id { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; } = null!;
        public int SalaId { get; set; }
        public Sala Sala { get; set; } = null!;
        public DateTime FechaFuncion { get; set; }
        public TimeSpan HoraFuncion { get; set; }
        public bool Estado { get; set; } = true; // true = activa
    }

}
