import { createContext, useContext, useState } from 'react';
import type { ReactNode } from 'react';
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

interface ReservationContextType {
    reservas: Reserva[];
    obtenerReservasTerror: (desde: string, hasta: string) => Promise<void>;
}

const ReservationContext = createContext<ReservationContextType | undefined>(undefined);

export const ReservationProvider = ({ children }: { children: ReactNode }) => {
    const [reservas, setReservas] = useState<Reserva[]>([]);

    const obtenerReservasTerror = async (desde: string, hasta: string) => {
        try {
            const response = await axios.get('http://localhost:5117/api/Cartelera/reservas-terror', {
                params: { desde, hasta },
            });
            setReservas(response.data);
        } catch (error) {
            console.error('Error al obtener reservas de terror', error);
        }
    };

    return (
        <ReservationContext.Provider value={{ reservas, obtenerReservasTerror }}>
            {children}
        </ReservationContext.Provider>
    );
};

export const useReservation = () => {
    const context = useContext(ReservationContext);
    if (!context) {
        throw new Error('useReservation debe usarse dentro de ReservationProvider');
    }
    return context;
};
