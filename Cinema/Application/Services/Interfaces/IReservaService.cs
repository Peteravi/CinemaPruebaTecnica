using Cinema.Entities;

namespace Cinema.Application.Services.Interfaces
{
    public interface IReservaService
    {
        Task CancelarReservaAsync(int reservaId);
        Task CancelarCarteleraAsync(int carteleraId);
        Task<IEnumerable<Cliente>> ObtenerClientesAfectadosPorCancelacionAsync(int carteleraId);
    }
}
