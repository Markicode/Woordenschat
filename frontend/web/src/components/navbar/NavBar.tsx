import { Link, useLocation } from "react-router-dom";
import styles from "./NavBar.module.css";

export default function NavBar() {
  const { pathname } = useLocation();
  const isHome = pathname === "/";

  return (
    <nav className={`${styles.nav} ${isHome ? styles.overlay : styles.normal}`}>
      <div className={styles.logo}>Woordenschat</div>
      <div className={styles.links}>
        <Link to="/">Home</Link>
        <Link to="/library">Bibliotheek</Link>
        <Link to="/login">Login</Link>
      </div>
    </nav>
  );
}