import { useEffect, useState, type FormEvent } from 'react'
import base from './ModalAdicionar.module.css'
import styles from './ModalCriar.module.css'

export type CampoCriar =
  | { chave: string; rotulo: string; tipo: 'texto' | 'area'; obrigatorio?: boolean; max?: number; placeholder?: string }
  | { chave: string; rotulo: string; tipo: 'numero'; padrao?: number }
  | { chave: string; rotulo: string; tipo: 'opcoes'; opcoes: { valor: number; rotulo: string }[] }

export type Valores = Record<string, string | number>

function iniciais(campos: CampoCriar[]): Valores {
  const v: Valores = {}
  for (const c of campos) {
    if (c.tipo === 'numero') v[c.chave] = c.padrao ?? 0
    else if (c.tipo === 'opcoes') v[c.chave] = c.opcoes[0]?.valor ?? 0
    else v[c.chave] = ''
  }
  return v
}

export default function ModalCriar({ titulo, campos, onConfirmar, onFechar }: {
  titulo: string
  campos: CampoCriar[]
  onConfirmar: (valores: Valores) => Promise<void>
  onFechar: () => void
}) {
  const [valores, setValores] = useState<Valores>(() => iniciais(campos))
  const [erro, setErro] = useState<string | null>(null)
  const [salvando, setSalvando] = useState(false)

  useEffect(() => {
    const fechar = (e: KeyboardEvent) => { if (e.key === 'Escape') onFechar() }
    window.addEventListener('keydown', fechar)
    return () => window.removeEventListener('keydown', fechar)
  }, [onFechar])

  const mudar = (chave: string, valor: string | number) =>
    setValores(prev => ({ ...prev, [chave]: valor }))

  const enviar = async (e: FormEvent) => {
    e.preventDefault()
    for (const c of campos) {
      if ((c.tipo === 'texto' || c.tipo === 'area') && c.obrigatorio && !String(valores[c.chave]).trim()) {
        setErro(`O campo "${c.rotulo}" é obrigatório.`)
        return
      }
    }
    setSalvando(true)
    setErro(null)
    try {
      await onConfirmar(valores)
      onFechar()
    } catch (err) {
      const t = err instanceof Error ? err.message : ''
      setErro(t && t.length < 200 ? t : 'Não foi possível salvar.')
      setSalvando(false)
    }
  }

  return (
    <div className={base.fundo} onClick={onFechar}>
      <form className={base.janela} onClick={e => e.stopPropagation()} onSubmit={enviar}
            noValidate role="dialog" aria-label={titulo}>
        <header className={base.topo}>
          <h2 className={base.titulo}>{titulo}</h2>
          <button type="button" className={base.fechar} onClick={onFechar} title="Fechar">✕</button>
        </header>

        <div className={`${base.corpo} ${styles.campos}`}>
          {campos.map((c, i) => (
            <label key={c.chave} className={styles.campo}>
              <span className={styles.rotulo}>{c.rotulo}</span>

              {c.tipo === 'texto' && (
                <input className={styles.input} value={String(valores[c.chave])}
                       maxLength={c.max} placeholder={c.placeholder}
                       autoFocus={i === 0} autoComplete="off"
                       onChange={e => mudar(c.chave, e.target.value)} />
              )}

              {c.tipo === 'area' && (
                <textarea className={styles.area} rows={3} value={String(valores[c.chave])}
                          maxLength={c.max} placeholder={c.placeholder}
                          onChange={e => mudar(c.chave, e.target.value)} />
              )}

              {c.tipo === 'numero' && (
                <input className={styles.input} type="number" value={Number(valores[c.chave])}
                       onChange={e => mudar(c.chave, Number(e.target.value))} />
              )}

              {c.tipo === 'opcoes' && (
                <select className={styles.input} value={Number(valores[c.chave])}
                        onChange={e => mudar(c.chave, Number(e.target.value))}>
                  {c.opcoes.map(o => <option key={o.valor} value={o.valor}>{o.rotulo}</option>)}
                </select>
              )}
            </label>
          ))}
        </div>

        {erro && <small className={base.erro}>{erro}</small>}

        <footer className={base.rodape}>
          <button type="button" className={styles.cancelar} onClick={onFechar}>Cancelar</button>
          <button type="submit" className={base.confirmar} disabled={salvando}>
            {salvando ? 'Salvando...' : 'Criar'}
          </button>
        </footer>
      </form>
    </div>
  )
}