using Cinema.Entities;

namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IReservaRepository : IBaseRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> ObtenerReservasPorGeneroYFechas(string genero, DateTime desde, DateTime hasta);
        Task<IEnumerable<Cliente>> ObtenerClientesAfectadosPorCartelera(int carteleraId);
    }
}
