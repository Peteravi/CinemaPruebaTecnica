using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class CarteleraController : ControllerBase
{
    private readonly IReservaService _reservaService;
    private readonly ApplicationDbContext _context;

    public CarteleraController(IReservaService reservaService, ApplicationDbContext context)
    {
        _reservaService = reservaService;
        _context = context;
    }

    [HttpPost("cancelar-cartelera/{id}")]
    public async Task<IActionResult> CancelarCartelera(int id)
    {
        await _reservaService.CancelarCarteleraAsync(id);
        return Ok(new { mensaje = "Cartelera cancelada correctamente" });
    }

    [HttpGet("reservas-terror")]
    public async Task<IActionResult> ObtenerReservasTerror(DateTime desde, DateTime hasta)
    {
        var reservas = await _context.Reservas
            .Include(r => r.Cartelera).ThenInclude(c => c.Pelicula)
            .Include(r => r.Cliente)
            .Where(r => r.Cartelera.Pelicula.Genero.ToLower() == "terror" &&
                        r.Cartelera.FechaFuncion >= desde &&
                        r.Cartelera.FechaFuncion <= hasta)
            .ToListAsync();

        return Ok(reservas);
    }

    [HttpGet("butacas-estado")]
    public async Task<IActionResult> ButacasDisponiblesYOcupadasHoy()
    {
        var hoy = DateTime.Today;

        var resumen = await _context.Carteleras
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

        return Ok(resumen);
    }
}
