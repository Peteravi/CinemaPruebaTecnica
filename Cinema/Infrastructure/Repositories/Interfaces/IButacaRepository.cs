using Cinema.Entities;

namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IButacaRepository : IBaseRepository<Butaca>
    {
        Task InhabilitarButacaAsync(int id);
    }
}
