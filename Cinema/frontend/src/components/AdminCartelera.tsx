import { useState } from 'react';
import axios, { AxiosError } from 'axios';

export default function AdminCartelera() {
    const [carteleraId, setCarteleraId] = useState<number>(0);
    const [mensaje, setMensaje] = useState('');

    const cancelarCartelera = async () => {
        try {
            await axios.post(`http://localhost:5117/api/Cartelera/cancelar-cartelera/${carteleraId}`);
            setMensaje('Cartelera cancelada correctamente.');
        } catch (error) {
            const axiosError = error as AxiosError<{ error: string }>;
            const mensajeError = axiosError.response?.data?.error || 'Error al cancelar cartelera';
            setMensaje(mensajeError);
        }
    };

    return (
        <div className="p-4">
            <h2 className="text-xl font-bold mb-4">Cancelar Cartelera</h2>
            <input
                type="number"
                value={carteleraId}
                onChange={(e) => setCarteleraId(Number(e.target.value))}
                className="border p-2 rounded mr-2"
                placeholder="ID de cartelera"
            />
            <button
                onClick={cancelarCartelera}
                className="bg-red-600 text-white px-4 py-2 rounded"
            >
                Cancelar
            </button>
            {mensaje && <p className="mt-4 text-blue-700">{mensaje}</p>}
        </div>
    );
}
