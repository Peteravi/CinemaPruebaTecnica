import axios from 'axios';

// URL base de tu backend .NET
const API_URL = 'http://localhost:5117/api';

export const reservationService = {
    // Obtener reservas de películas de terror en un rango de fechas
    getReservasTerror: async (desde: string, hasta: string) => {
        const response = await axios.get(`${API_URL}/Cartelera/reservas-terror`, {
            params: { desde, hasta },
        });
        return response.data;
    },

    // Cancelar una cartelera
    cancelarCartelera: async (carteleraId: number) => {
        const response = await axios.post(`${API_URL}/Cartelera/cancelar-cartelera/${carteleraId}`);
        return response.data;
    },

    // Inhabilitar una butaca
    inhabilitarButaca: async (butacaId: number) => {
        const response = await axios.post(`${API_URL}/Butaca/inhabilitar/${butacaId}`);
        return response.data;
    },

    // Obtener butacas por sala
    getButacasPorSala: async (salaId: number) => {
        const response = await axios.get(`${API_URL}/Butaca/por-sala/${salaId}`);
        return response.data;
    }
};
