import { Link } from 'react-router-dom'
import type { PersonagemDto } from '../types'
import styles from './PersonagemCard.module.css'

export default function PersonagemCard({ p, idCampanha }: { p: PersonagemDto; idCampanha: number }) {  const temVida = p.vidaAtual !== undefined && !!p.vidaMaxima
  const pct = temVida ? Math.min(100, Math.max(0, (p.vidaAtual! / p.vidaMaxima!) * 100)) : 0

  return (
    <Link to={`/personagens/${p.id_Personagem}/${idCampanha}`} className={styles.card}>
      {p.fotoUrl ? (
        <img className={styles.foto} src={p.fotoUrl} alt="" />
      ) : (
        <div className={styles.foto} aria-hidden="true">
          {p.nome.charAt(0).toUpperCase()}
        </div>
      )}

      <h3 className={styles.nome}>{p.nome}</h3>
      <p className={styles.info}>{p.ehNpc ? 'NPC' : (p.nomeJogador ?? 'Jogador')}</p>
      <p className={styles.info}>Nível {p.nivel}</p>

      <div className={styles.barra}>
        <div className={pct < 30 ? styles.vidaBaixa : styles.vida} style={{ width: `${pct}%` }} />
      </div>
      <span className={styles.vidaTexto}>{temVida ? `${p.vidaAtual}/${p.vidaMaxima}` : 'Vida: —'}</span>
    </Link>
  )
}