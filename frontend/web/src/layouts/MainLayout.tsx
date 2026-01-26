import { Outlet, useLocation } from "react-router-dom";
import NavBar from "../components/navbar/NavBar.tsx";
import styles from "./MainLayout.module.css";
import PageBanner from "../components/pagebanner/PageBanner.tsx";

export default function MainLayout() {
  const { pathname } = useLocation();
  const isHome = pathname === "/";

  return (
    <div className={styles.container}>
      {!isHome && <PageBanner />}
      <NavBar />
      <main className={styles.main}>
        <Outlet />
      </main>
    </div>
  );
}
