import { NavLink, useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import styles from "./NavBar.module.css";

export default function NavBar() {
  const { pathname } = useLocation();
  const isHome = pathname === "/";

  const [pastHero, setPastHero] = useState(false);

  useEffect(() => {
    if (!isHome) return;

    const hero = document.getElementById("hero");
    if (!hero) return;

    const observer = new IntersectionObserver(
      ([entry]) => {
        setPastHero(!entry.isIntersecting);
      },
      { threshold: 0.1 }
    );

    observer.observe(hero);

    return () => observer.disconnect();
  }, [isHome]);

  return (
    <nav
      className={`
        ${styles.nav}
        ${isHome ? styles.fixed : ""}
        ${isHome && !pastHero ? styles.overlay : styles.scrolled}
        ${!isHome ? styles.normal : ""}
      `}
    >
      <div className={styles.logo}>Woordenschat</div>

      <div className={styles.links}>
        <NavLink
          to="/"
          className={({ isActive }) => (isActive ? styles.active : undefined)}
        >
          Home
        </NavLink>
        <NavLink
          to="/books"
          className={({ isActive }) => (isActive ? styles.active : undefined)}
        >
          Boeken
        </NavLink>
        <NavLink
          to="/login"
          className={({ isActive }) => (isActive ? styles.active : undefined)}
        >
          Login
        </NavLink>
      </div>
    </nav>
  );
}
