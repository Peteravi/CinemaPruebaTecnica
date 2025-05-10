using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class ButacaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ButacaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("por-sala/{salaId}")]
    public async Task<IActionResult> ObtenerButacasPorSala(int salaId)
    {
        var butacas = await _context.Butacas
            .Where(b => b.SalaId == salaId)
            .ToListAsync();

        return Ok(butacas);
    }

    [HttpPost("inhabilitar/{id}")]
    public async Task<IActionResult> InhabilitarButaca(int id)
    {
        var butaca = await _context.Butacas.FindAsync(id);
        if (butaca == null) return NotFound();

        butaca.Estado = false;
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Butaca inhabilitada correctamente" });
    }
}
