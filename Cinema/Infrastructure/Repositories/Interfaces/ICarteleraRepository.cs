using Cinema.Entities;

namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface ICarteleraRepository : IBaseRepository<Cartelera>
    {
        Task<IEnumerable<object>> ObtenerEstadoButacasPorSalaHoy();
    }
}
