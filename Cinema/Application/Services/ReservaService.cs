using Cinema.Application.Services.Interfaces;
using Cinema.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;

namespace Cinema.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IButacaRepository _butacaRepo;
        private readonly ICarteleraRepository _carteleraRepo;

        public ReservaService(
            IReservaRepository reservaRepo,
            IButacaRepository butacaRepo,
            ICarteleraRepository carteleraRepo)
        {
            _reservaRepo = reservaRepo;
            _butacaRepo = butacaRepo;
            _carteleraRepo = carteleraRepo;
        }

        public async Task CancelarReservaAsync(int reservaId)
        {
            var reserva = await _reservaRepo.GetByIdAsync(reservaId);
            if (reserva == null)
                throw new Exception("Reserva no encontrada");

            reserva.Estado = "cancelada";

            // Habilitar la butaca asociada
            var butaca = reserva.Butaca;
            if (butaca != null)
            {
                butaca.Estado = true;
                _butacaRepo.Update(butaca);
            }

            _reservaRepo.Update(reserva);
            await _reservaRepo.SaveChangesAsync();
        }

        public async Task CancelarCarteleraAsync(int carteleraId)
        {
            var cartelera = await _carteleraRepo.GetByIdAsync(carteleraId);
            if (cartelera == null)
                throw new Exception("Cartelera no encontrada");

            if (cartelera.FechaFuncion < DateTime.Today)
                throw new InvalidOperationException("No se puede cancelar funciones de la cartelera con fecha anterior a la actual.");

            cartelera.Estado = false;
            _carteleraRepo.Update(cartelera);

            var reservas = await _reservaRepo.FindAsync(r => r.CarteleraId == carteleraId);

            foreach (var reserva in reservas)
            {
                reserva.Estado = "cancelada";

                var butaca = reserva.Butaca;
                if (butaca != null)
                {
                    butaca.Estado = true;
                    _butacaRepo.Update(butaca);
                }

                _reservaRepo.Update(reserva);
            }

            await _reservaRepo.SaveChangesAsync();

            var clientes = await ObtenerClientesAfectadosPorCancelacionAsync(carteleraId);
            Console.WriteLine("Clientes afectados:");
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"{cliente.Nombre} - {cliente.Correo}");
            }
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesAfectadosPorCancelacionAsync(int carteleraId)
        {
            return await _reservaRepo.ObtenerClientesAfectadosPorCartelera(carteleraId);
        }
    }
}
