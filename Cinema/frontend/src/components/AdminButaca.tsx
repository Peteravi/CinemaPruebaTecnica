import { useState, useEffect } from 'react';
import axios from 'axios';

interface Butaca {
  id: number;
  numero: number;
  fila: string;
  estado: boolean;
}

export default function AdminButaca() {
  const [butacas, setButacas] = useState<Butaca[]>([]);
  const [salaId, setSalaId] = useState(1);

  const obtenerButacas = async () => {
    try {
      const response = await axios.get(`http://localhost:5117/api/Butaca/por-sala/${salaId}`);
      setButacas(response.data);
    } catch (error) {
      console.error('Error al obtener butacas', error);
    }
  };

  const inhabilitar = async (id: number) => {
    try {
      await axios.post(`http://localhost:5117/api/Butaca/inhabilitar/${id}`);
      obtenerButacas();
    } catch (error) {
      console.error('Error al inhabilitar butaca', error);
    }
  };

  useEffect(() => {
    obtenerButacas();
  }, [salaId]);

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Administrar Butacas (Sala {salaId})</h2>
      <div className="mb-4">
        <label htmlFor="salaId">ID de Sala: </label>
        <input
          id="salaId"
          type="number"
          value={salaId}
          onChange={(e) => setSalaId(Number(e.target.value))}
          className="border p-1 rounded ml-2"
        />
      </div>
      <table className="w-full border">
        <thead>
          <tr>
            <th>ID</th>
            <th>Número</th>
            <th>Fila</th>
            <th>Estado</th>
            <th>Acción</th>
          </tr>
        </thead>
        <tbody>
          {butacas.map((b) => (
            <tr key={b.id} className="text-center border-t">
              <td>{b.id}</td>
              <td>{b.numero}</td>
              <td>{b.fila}</td>
              <td>{b.estado ? 'Disponible' : 'Inhabilitada'}</td>
              <td>
                <button
                  onClick={() => inhabilitar(b.id)}
                  className="bg-red-500 text-white px-2 py-1 rounded"
                  disabled={!b.estado}
                >
                  Inhabilitar
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
