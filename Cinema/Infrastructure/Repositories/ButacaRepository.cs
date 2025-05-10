using Cinema.Entities;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class ButacaRepository : BaseRepository<Butaca>, IButacaRepository
    {
        private readonly ApplicationDbContext _context;

        public ButacaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task InhabilitarButacaAsync(int id)
        {
            var butaca = await _context.Butacas.FindAsync(id);
            if (butaca != null)
            {
                butaca.Estado = false;
                _context.Update(butaca);
                await _context.SaveChangesAsync();
            }
        }
    }
}
