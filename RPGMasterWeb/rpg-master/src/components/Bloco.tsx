import type { ReactNode } from 'react'
import styles from './Bloco.module.css'

interface BlocoProps {
  titulo: string
  podeAdicionar: boolean
  onAdicionar?: () => void
  loading?: boolean
  erro?: string | null
  vazio?: boolean
  textoVazio?: string
  children: ReactNode
}

export default function Bloco({
  titulo,
  podeAdicionar,
  onAdicionar,
  loading,
  erro,
  vazio,
  textoVazio = 'Nada registrado ainda.',
  children,
}: BlocoProps) {
  return (
    <section className={styles.bloco}>
      <header className={styles.cabecalho}>
        <h2 className={styles.titulo}>{titulo}</h2>
        {podeAdicionar && (
          <button className={styles.adicionar} onClick={onAdicionar}>
            + Adicionar
          </button>
        )}
      </header>

      {loading && <p className={styles.aviso}>Carregando...</p>}
      {erro && <p className={styles.erro}>{erro}</p>}
      {!loading && !erro && vazio && <p className={styles.aviso}>{textoVazio}</p>}
      {!loading && !erro && !vazio && children}
    </section>
  )
}