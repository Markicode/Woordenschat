import styles from './Hero.module.css';

export default function Hero() {

    return (
    <section className={styles.hero}>
      <div className={styles.content}>
        <h1><span className={styles.titleLeft}>Bibliotheek</span> <span className={styles.titleRight}>Woordenschat</span></h1>
        <p>Ontdek de rijkdom van taal.</p>
      </div>
    </section>
  );
}