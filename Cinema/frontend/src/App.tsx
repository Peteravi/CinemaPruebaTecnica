import AppRouter from './routes/AppRouter';
import { ReservationProvider } from './context/ReservationContext';

function App() {
    return (
        <ReservationProvider>
            <AppRouter />
        </ReservationProvider>
    );
}

export default App;
