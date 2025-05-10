import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import AdminCartelera from '../components/AdminCartelera';
import AdminButaca from '../components/AdminButaca';
import ReservationList from '../components/ReservationList';

export default function AppRouter() {
    return (
        <Router>
            <div className="min-h-screen bg-gray-100">
                <nav className="bg-blue-600 p-4 text-white flex gap-4">
                    <Link to="/cartelera" className="hover:underline">Cartelera</Link>
                    <Link to="/butacas" className="hover:underline">Butacas</Link>
                    <Link to="/reservas" className="hover:underline">Reservas Terror</Link>
                </nav>

                <div className="p-6">
                    <Routes>
                        <Route path="/" element={<Navigate to="/cartelera" />} />
                        <Route path="/cartelera" element={<AdminCartelera />} />
                        <Route path="/butacas" element={<AdminButaca />} />
                        <Route path="/reservas" element={<ReservationList />} />
                        <Route path="*" element={<h2>Página no encontrada</h2>} />
                    </Routes>
                </div>
            </div>
        </Router>
    );
}
