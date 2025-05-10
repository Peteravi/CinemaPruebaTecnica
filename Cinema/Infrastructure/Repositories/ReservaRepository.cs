using Cinema.Entities;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> ObtenerReservasPorGeneroYFechas(string genero, DateTime desde, DateTime hasta)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Cartelera)
                    .ThenInclude(c => c.Pelicula)
                .Where(r => r.Cartelera.Pelicula.Genero.ToLower() == genero.ToLower()
                         && r.Cartelera.FechaFuncion >= desde
                         && r.Cartelera.FechaFuncion <= hasta)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesAfectadosPorCartelera(int carteleraId)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Where(r => r.CarteleraId == carteleraId)
                .Select(r => r.Cliente)
                .Distinct()
                .ToListAsync();
        }
    }
}
