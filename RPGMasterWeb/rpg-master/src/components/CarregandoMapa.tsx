import styles from './CarregandoMapa.module.css'

export default function CarregandoMapa({ texto = 'Carregando...' }: { texto?: string }) {
  return (
    <div className={styles.wrap} role="status" aria-live="polite">
      <div className={styles.mapa}>
        <div className={styles.rolo} />
        <div className={styles.folha}>
          <svg viewBox="0 0 120 80" width="120" height="80" aria-hidden="true">
            <path className={styles.rota} pathLength="100"
                  d="M14 60 C 34 20, 52 70, 74 36 S 100 34, 104 22" />
            <path className={styles.xis} d="M98 16 L110 28 M110 16 L98 28" />
          </svg>
        </div>
        <div className={styles.rolo} />
      </div>
      <p className={styles.texto}>{texto}</p>
    </div>
  )
}