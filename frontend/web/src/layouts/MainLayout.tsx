import { Outlet } from 'react-router-dom';
import NavBar from '../components/navbar/NavBar.tsx';
import styles from './MainLayout.module.css';

export default function MainLayout() {
    return (
    <div className={styles.container}>
      <NavBar />
      <main className={styles.main}>
        <Outlet />
      </main>
    </div>
    );
}