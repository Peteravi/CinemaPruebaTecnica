import { useState } from 'react';
import axios from 'axios';

interface Cliente {
    nombre: string;
    correo: string;
}

interface Reserva {
    id: number;
    estado: string;
    fechaReserva: string;
    cliente: Cliente;
    cartelera: {
        fechaFuncion: string;
        pelicula: { titulo: string };
    };
}

export default function ReservationList() {
    const [reservas, setReservas] = useState<Reserva[]>([]);
    const [desde, setDesde] = useState('');
    const [hasta, setHasta] = useState('');

    const obtenerReservas = async () => {
        try {
            const response = await axios.get(`http://localhost:5117/api/Cartelera/reservas-terror`, {
                params: { desde, hasta },
            });
            setReservas(response.data);
        } catch (error) {
            console.error('Error al obtener reservas de terror', error);
        }
    };

    return (
        <div className="p-4">
            <h2 className="text-xl font-bold mb-4">Reservas de películas de Terror</h2>
            <div className="mb-4 flex gap-2">
                <input
                    type="date"
                    value={desde}
                    onChange={(e) => setDesde(e.target.value)}
                    className="border p-1 rounded"
                />
                <input
                    type="date"
                    value={hasta}
                    onChange={(e) => setHasta(e.target.value)}
                    className="border p-1 rounded"
                />
                <button
                    onClick={obtenerReservas}
                    className="bg-blue-600 text-white px-3 py-1 rounded"
                >
                    Buscar
                </button>
            </div>

            <table className="w-full border">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Cliente</th>
                        <th>Correo</th>
                        <th>Película</th>
                        <th>Fecha Función</th>
                        <th>Estado</th>
                    </tr>
                </thead>
                <tbody>
                    {reservas.map((res) => (
                        <tr key={res.id} className="text-center border-t">
                            <td>{res.id}</td>
                            <td>{res.cliente.nombre}</td>
                            <td>{res.cliente.correo}</td>
                            <td>{res.cartelera.pelicula.titulo}</td>
                            <td>{new Date(res.cartelera.fechaFuncion).toLocaleDateString()}</td>
                            <td>{res.estado}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
