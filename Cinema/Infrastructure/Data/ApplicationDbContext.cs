using Microsoft.EntityFrameworkCore;
using Cinema.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Pelicula> Peliculas => Set<Pelicula>();
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<Butaca> Butacas => Set<Butaca>();
    public DbSet<Cartelera> Carteleras => Set<Cartelera>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
}
