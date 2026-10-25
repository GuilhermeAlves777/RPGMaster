import { useState } from 'react'
import type { FormEvent } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'
import styles from './Login.module.css'
import { Link } from 'react-router-dom'

export default function Login() {
  const { login } = useAuth()
  const navigate = useNavigate()
  const [usuario, setUsuario] = useState('')
  const [senha, setSenha] = useState('')
  const [erro, setErro] = useState<string | null>(null)

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setErro(null)
    try {
      await login(usuario, senha)
      const destino = sessionStorage.getItem('destinoPosLogin')
      sessionStorage.removeItem('destinoPosLogin')
      navigate(destino && destino.startsWith('/convite/') ? 
        destino : '/campanhas', { replace: true })
    } catch (err) {
      setErro(err instanceof Error ? err.message : 'Não foi possível entrar')
    }
  }

  return (
    <main className={styles.page}>
      {/* rota tracejada do mapa */}
      <svg className={styles.route} viewBox="0 0 1000 600" preserveAspectRatio="none" aria-hidden="true">
        <path d="M 60 520 C 250 380, 300 560, 480 420 S 760 200, 940 90" />
      </svg>

      {/* rosa dos ventos */}
      <svg className={styles.compass} viewBox="-50 -50 100 100" aria-hidden="true">
        <circle r="40" fill="none" stroke="currentColor" strokeWidth="1.5" />
        <circle r="34" fill="none" stroke="currentColor" strokeWidth="0.6" />
        <polygon points="0,-46 7,0 0,46 -7,0" fill="currentColor" opacity="0.85" />
        <polygon points="-46,0 0,-7 46,0 0,7" fill="currentColor" opacity="0.5" />
        <text y="-50" textAnchor="middle" fontSize="10" fill="currentColor">N</text>
      </svg>

      <form className={styles.card} onSubmit={handleSubmit}>
        <h1 className={styles.title}>RPGMaster</h1>
        <p className={styles.subtitle}>Entre e continue sua jornada</p>

        <label className={styles.field}>
          <span>Usuario</span>
          <input type="usuario" value={usuario} onChange={(e) => setUsuario(e.target.value)} required />
        </label>

        <label className={styles.field}>
          <span>Senha</span>
          <input type="password" value={senha} onChange={(e) => setSenha(e.target.value)} required />
        </label>

        <p className={styles.criarConta}>
          Ainda não tem conta? <Link to="/cadastro">Criar conta</Link>
        </p>

        <button className={styles.button} type="submit">Entrar</button>
        {erro && <p className={styles.erro}>{erro}</p>}
      </form>
    </main>
  )
}