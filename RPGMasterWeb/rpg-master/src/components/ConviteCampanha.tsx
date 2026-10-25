import { useEffect, useState } from 'react'
import { conviteService } from '../services/conviteService'
import styles from './ConviteCampanha.module.css'

export default function ConviteCampanha({ idCampanha }: { idCampanha: number }) {
  const [codigo, setCodigo] = useState<string | null>(null)
  const [carregando, setCarregando] = useState(true)
  const [gerando, setGerando] = useState(false)
  const [copiado, setCopiado] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  useEffect(() => {
    let ativo = true
    conviteService.obter(idCampanha)
      .then(c => { if (ativo) setCodigo(c || null) })
      .catch(() => { if (ativo) setCodigo(null) })   // sem convite ainda (204/404)
      .finally(() => { if (ativo) setCarregando(false) })
    return () => { ativo = false }
  }, [idCampanha])

  const link = codigo ? `${window.location.origin}/convite/${codigo}` : ''

  const gerar = async () => {
    if (codigo && !window.confirm('Gerar um novo link? O link atual deixará de funcionar.')) return
    setGerando(true)
    setErro(null)
    try {
      const novo = await conviteService.gerar(idCampanha)
      setCodigo(novo)
      setCopiado(false)
    } catch (e) {
      const texto = e instanceof Error ? e.message : ''
      setErro(texto && texto.length < 200 ? texto : 'Não foi possível gerar o convite.')
    } finally {
      setGerando(false)
    }
  }

  const copiar = async () => {
    try {
      await navigator.clipboard.writeText(link)
      setCopiado(true)
      setTimeout(() => setCopiado(false), 2000)
    } catch {
      setErro('Não foi possível copiar. Selecione o link e copie manualmente.')
    }
  }

  if (carregando) return <p className={styles.info}>Carregando...</p>

  return (
    <div className={styles.wrap}>
      {codigo ? (
        <>
          <div className={styles.linha}>
            <input className={styles.link} value={link} readOnly onFocus={e => e.target.select()} />
            <button className={styles.btn} onClick={copiar}>{copiado ? 'Copiado!' : 'Copiar'}</button>
          </div>
          <button className={styles.novo} onClick={gerar} disabled={gerando}>
            {gerando ? 'Gerando...' : 'Gerar novo link'}
          </button>
        </>
      ) : (
        <>
          <p className={styles.info}>Ainda não há um link de convite para esta campanha.</p>
          <button className={styles.btn} onClick={gerar} disabled={gerando}>
            {gerando ? 'Gerando...' : 'Gerar link de convite'}
          </button>
        </>
      )}
      {erro && <small className={styles.erro}>{erro}</small>}
    </div>
  )
}