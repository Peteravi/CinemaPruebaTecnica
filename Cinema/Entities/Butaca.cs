namespace Cinema.Entities
{
    public class Butaca
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Fila { get; set; } = string.Empty;
        public int SalaId { get; set; }
        public Sala Sala { get; set; } = null!;
        public bool Estado { get; set; } = true; // true = disponible
    }
}
