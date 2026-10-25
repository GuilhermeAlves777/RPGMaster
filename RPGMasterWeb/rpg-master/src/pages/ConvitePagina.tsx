import { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import CarregandoMapa from '../components/CarregandoMapa'
import { conviteService } from '../services/conviteService'
import type { ConviteDto } from '../types'
import styles from './ConvitePagina.module.css'

const limparDestino = () => sessionStorage.removeItem('destinoPosLogin')

export default function ConvitePagina() {
  const { codigo } = useParams()
  const navigate = useNavigate()
  const logado = !!localStorage.getItem('token')

  const [convite, setConvite] = useState<ConviteDto | null>(null)
  const [carregando, setCarregando] = useState(logado)
  const [problema, setProblema] = useState<string | null>(null)
  const [entrando, setEntrando] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  // guarda o endereço pra voltar aqui depois do login ou do cadastro
  useEffect(() => {
    if (codigo) sessionStorage.setItem('destinoPosLogin', `/convite/${codigo}`)
  }, [codigo])

  useEffect(() => {
    if (!logado || !codigo) return undefined
    let ativo = true
    conviteService.consultar(codigo)
      .then(c => {
        if (!ativo) return
        if (c) setConvite(c)
        else setProblema('Este convite não é mais válido.')
      })
      .catch(e => {
        if (!ativo) return
        const t = e instanceof Error ? e.message : ''
        setProblema(t && t.length < 200 ? t : 'Não foi possível verificar o convite.')
      })
      .finally(() => { if (ativo) setCarregando(false) })
    return () => { ativo = false }
  }, [codigo, logado])

  const entrar = async () => {
    if (!codigo || !convite) return
    setEntrando(true)
    setErro(null)
    try {
      await conviteService.aceitar(codigo)
      limparDestino()
      navigate(`/campanhas/${convite.id_Campanha}`, { replace: true })
    } catch (e) {
      const t = e instanceof Error ? e.message : ''
      setErro(t && t.length < 200 ? t : 'Não foi possível entrar na campanha.')
      setEntrando(false)
    }
  }

  return (
    <div className={styles.pagina}>
      <div className={styles.card}>
        <h1 className={styles.titulo}>RPGMaster</h1>

        {!logado && (
          <>
            <p className={styles.texto}>Você recebeu um convite para entrar em uma campanha.</p>
            <p className={styles.texto}>Entre ou crie uma conta para continuar.</p>
            <div className={styles.acoes}>
              <Link to="/login" className={styles.botao}>Entrar</Link>
              <Link to="/cadastro" className={styles.secundario}>Criar conta</Link>
            </div>
          </>
        )}

        {logado && carregando && <CarregandoMapa texto="Abrindo o convite..." />}

        {logado && !carregando && problema && (
          <>
            <p className={styles.erro}>{problema}</p>
            <Link to="/campanhas" className={styles.secundario} onClick={limparDestino}>
              Ir para minhas campanhas
            </Link>
          </>
        )}

        {logado && !carregando && convite && (
          <>
            <p className={styles.texto}>Você foi convidado para a campanha</p>
            <h2 className={styles.campanha}>{convite.campanha}</h2>
            {erro && <p className={styles.erro}>{erro}</p>}
            <div className={styles.acoes}>
              <button className={styles.botao} onClick={entrar} disabled={entrando}>
                {entrando ? 'Entrando...' : 'Entrar na campanha'}
              </button>
              <Link to="/campanhas" className={styles.secundario} onClick={limparDestino}>
                Agora não
              </Link>
            </div>
          </>
        )}
      </div>
    </div>
  )
}