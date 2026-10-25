import styles from './ControleValor.module.css'

const formatar = (v: number) => (v > 0 ? `+${v}` : `${v}`)

export function ControleValor({ valor, podeEditar, onChange, onExcluir }: {
  valor: number
  podeEditar: boolean
  onChange: (novo: number) => void
  onExcluir?: () => void
}) {
  return (
    <div className={styles.controle}>
      <strong className={valor < 0 ? styles.negativo : valor > 0 ? styles.positivo : undefined}>
        {formatar(valor)}
      </strong>
      {podeEditar && (
        <>
          <button onClick={() => onChange(valor - 1)} title="Diminuir">−</button>
          <button onClick={() => onChange(valor + 1)} title="Aumentar">+</button>
          {onExcluir && (
            <button className={styles.excluir} onClick={onExcluir} title="Excluir">✕</button>
          )}
        </>
      )}
    </div>
  )
}

interface CartaoProps {
  titulo: string
  podeEditar: boolean
  onAdicionar?: () => void
  vazio: boolean
  children: React.ReactNode
}

export function Cartao({ titulo, podeEditar, onAdicionar, vazio, children }: CartaoProps) {
  return (
    <section className="cartao">
      <header>
        <h3>{titulo}</h3>
        {podeEditar && <button onClick={onAdicionar}>+ Adicionar</button>}
      </header>
      {vazio ? <p className="vazio">Nada registrado ainda.</p> : children}
    </section>
  )
}