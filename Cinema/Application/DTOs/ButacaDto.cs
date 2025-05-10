namespace Cinema.Application.DTOs
{
    public class ButacaDto
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Fila { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public int SalaId { get; set; }
    }
}
