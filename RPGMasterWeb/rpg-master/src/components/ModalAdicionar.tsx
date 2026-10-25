import { useEffect, useState } from 'react'
import { useCarregar } from '../hooks/useCarregar'
import styles from './ModalAdicionar.module.css'

export interface Opcao {
  id: number
  nome: string
  valorPadrao?: number
  imagem?: string | null
}

export default function ModalAdicionar({
  titulo, rotuloValor = 'Valor', grade = false, carregar, jaPossui, pedeValor, onConfirmar, onFechar,
}: {
  titulo: string
  rotuloValor?: string
  grade?: boolean                          // item: quadrados com imagem
  carregar: () => Promise<Opcao[]>
  jaPossui: number[]                       // ids que o personagem já tem (ficam fora da lista)
  pedeValor: boolean                       // atributo, perícia e item: true. magia: false
  onConfirmar: (id: number, valor: number) => Promise<void>
  onFechar: () => void
}) {
  const lista = useCarregar(carregar, [])
  const [escolhido, setEscolhido] = useState<Opcao | null>(null)
  const [valor, setValor] = useState(0)
  const [erro, setErro] = useState<string | null>(null)
  const [salvando, setSalvando] = useState(false)

  useEffect(() => {
    const fechar = (e: KeyboardEvent) => { if (e.key === 'Escape') onFechar() }
    window.addEventListener('keydown', fechar)
    return () => window.removeEventListener('keydown', fechar)
  }, [onFechar])

  const disponiveis = (lista.dados ?? []).filter(o => !jaPossui.includes(o.id))

  const escolher = (o: Opcao) => {
    setEscolhido(o)
    setValor(o.valorPadrao ?? 0)
    setErro(null)
  }

  const confirmar = async () => {
    if (!escolhido) return
    setSalvando(true)
    setErro(null)
    try {
      await onConfirmar(escolhido.id, valor)
      onFechar()
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Não foi possível adicionar')
    } finally {
      setSalvando(false)
    }
  }

  return (
    <div className={styles.fundo} onClick={onFechar}>
      <div className={styles.janela} onClick={e => e.stopPropagation()} role="dialog" aria-label={titulo}>
        <header className={styles.topo}>
          <h2 className={styles.titulo}>{titulo}</h2>
          <button className={styles.fechar} onClick={onFechar} title="Fechar">✕</button>
        </header>

        <div className={styles.corpo}>
          {lista.erro && <p className={styles.erro}>{lista.erro}</p>}
          {lista.loading && !lista.dados && <p>Carregando...</p>}
          {lista.dados && disponiveis.length === 0 && (
            <p className={styles.vazio}>Nada disponível para adicionar.</p>
          )}

          {grade ? (
            <div className={styles.grade}>
              {disponiveis.map(o => (
                <button
                  key={o.id}
                  type="button"
                  className={o.id === escolhido?.id ? styles.cartaoAtivo : styles.cartao}
                  onClick={() => escolher(o)}
                  title={o.nome}
                >
                  <div className={styles.miniatura}>
                    {o.imagem ? (
                      <img src={o.imagem} alt={o.nome} />
                    ) : (
                      <span aria-hidden="true">{o.nome.charAt(0).toUpperCase()}</span>
                    )}
                  </div>
                  <span className={styles.nomeItem}>{o.nome}</span>
                </button>
              ))}
            </div>
          ) : (
            <ul className={styles.lista}>
              {disponiveis.map(o => (
                <li key={o.id}>
                  <button
                    className={o.id === escolhido?.id ? styles.itemAtivo : styles.item}
                    onClick={() => escolher(o)}
                  >
                    {o.nome}
                  </button>
                </li>
              ))}
            </ul>
          )}
        </div>

        <footer className={styles.rodape}>
          {pedeValor && (
            <label className={styles.valor}>
              <span>{rotuloValor}</span>
              <input
                type="number"
                value={valor}
                disabled={!escolhido}
                onChange={e => setValor(Number(e.target.value))}
              />
            </label>
          )}
          <button className={styles.confirmar} disabled={!escolhido || salvando} onClick={confirmar}>
            {salvando ? 'Adicionando...' : 'Adicionar'}
          </button>
        </footer>
        {erro && <small className={styles.erro}>{erro}</small>}
      </div>
    </div>
  )
}