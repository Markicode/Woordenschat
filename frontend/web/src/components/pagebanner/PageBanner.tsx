import styles from "./PageBanner.module.css";
import logo from "../../assets/img/Woordenschat-logo-png.png";

export default function PageBanner() {
  return (
    <div className={styles.banner}>
      <div className={styles.overlay} />
      <div className={styles.content}>
        <img src={logo} alt="Woordenschat logo" />
        <span>Bibliotheek Woordenschat</span>
      </div>
    </div>
  );
}
