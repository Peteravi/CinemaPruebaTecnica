using Cinema.Entities;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class CarteleraRepository : BaseRepository<Cartelera>, ICarteleraRepository
    {
        private readonly ApplicationDbContext _context;

        public CarteleraRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> ObtenerEstadoButacasPorSalaHoy()
        {
            var hoy = DateTime.Today;

            return await _context.Carteleras
                .Where(c => c.FechaFuncion == hoy)
                .Include(c => c.Sala)
                .Select(c => new
                {
                    Sala = c.Sala.Nombre,
                    Total = _context.Butacas.Count(b => b.SalaId == c.SalaId),
                    Ocupadas = _context.Reservas
                        .Count(r => r.CarteleraId == c.Id && r.Estado == "activa"),
                    Disponibles = _context.Butacas
                        .Count(b => b.SalaId == c.SalaId && b.Estado == true)
                }).ToListAsync();
        }
    }
}
