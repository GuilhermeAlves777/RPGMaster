import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'
import styles from './Header.module.css'

export default function Header() {
  const { usuario, logout } = useAuth()
  const navigate = useNavigate()
  const inicial = usuario?.nome.charAt(0).toUpperCase() ?? '?'

  function handleSair() {
    logout()
    navigate('/login')
  }

  return (
    <header className={styles.header}>
      <Link to="/campanhas" className={styles.brand}>RPGMaster</Link>

      <div className={styles.user}>
        {usuario?.fotoUrl ? (
          <img className={styles.avatar} src={usuario.fotoUrl} alt="" />
        ) : (
          <div className={styles.avatar} aria-hidden="true">{inicial}</div>
        )}
        <span className={styles.nome}>{usuario?.nome}</span>
        <button className={styles.sair} onClick={handleSair}>Sair</button>
      </div>
    </header>
  )
}