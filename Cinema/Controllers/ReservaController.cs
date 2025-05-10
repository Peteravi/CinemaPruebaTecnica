using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservaController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpPost("cancelar-reserva/{id}")]
    public async Task<IActionResult> CancelarReserva(int id)
    {
        await _reservaService.CancelarReservaAsync(id);
        return Ok(new { mensaje = "Reserva cancelada y butaca liberada." });
    }
}
