import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'
import { campanhaService } from '../services/campanhaService'
import type { CampanhaDto } from '../types'
import styles from './Campanhas.module.css'

export default function Campanhas() {
  const { usuario } = useAuth()
  const idJogador = usuario?.id

  const [campanhas, setCampanhas] = useState<CampanhaDto[]>([])
  const [loading, setLoading] = useState(true)
  const [erro, setErro] = useState<string | null>(null)

  useEffect(() => {
    if (idJogador === undefined || !Number.isFinite(idJogador)) {
      setErro('Não foi possível identificar o jogador')
      setLoading(false)
      return
    }

    campanhaService
      .listarPorJogador(idJogador)
      .then(setCampanhas)
      .catch((e) => setErro(e instanceof Error ? e.message : 'Erro ao carregar campanhas'))
      .finally(() => setLoading(false))
  }, [idJogador])

  return (
    <section>
      <h1 className={styles.titulo}>Mapa de Campanhas</h1>
      <p className={styles.subtitulo}>Escolha um território para explorar</p>
      <div className={styles.acoesTopo}>
        <Link to="/campanhas/nova" className={styles.botaoNova}>+ Nova campanha</Link>
      </div>

      {loading && <p className={styles.aviso}>Desenrolando o mapa...</p>}
      {erro && <p className={styles.erro}>{erro}</p>}
      {!loading && !erro && campanhas.length === 0 && (
        <p className={styles.aviso}>Nenhum território descoberto ainda.</p>
      )}

      <ul className={styles.grid}>
        {campanhas.map((c) => (
          <li key={c.id_Campanha}>
            <Link to={`/campanhas/${c.id_Campanha}`} className={styles.card}>
              <span className={styles.pin} aria-hidden="true" />
              <h2 className={styles.nome}>{c.nome}</h2>
              {/* {c.descricao && <p className={styles.descricao}>{c.descricao}</p>} */}
            </Link>
          </li>
        ))}
      </ul>
    </section>
  )
}