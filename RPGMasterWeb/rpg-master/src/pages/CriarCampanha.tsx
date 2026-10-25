import { useState, type FormEvent } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import CarregandoMapa from '../components/CarregandoMapa'
import { campanhaService } from '../services/campanhaService'
import styles from './CriarCampanha.module.css'

const MIN = 3
const MAX = 100

export default function CriarCampanha() {
  const navigate = useNavigate()
  const [nome, setNome] = useState('')
  const [tocado, setTocado] = useState(false)
  const [erro, setErro] = useState<string | null>(null)
  const [enviando, setEnviando] = useState(false)

  const nomeLimpo = nome.trim()
  const nomeInvalido = nomeLimpo.length < MIN
  const mostrarErroCampo = tocado && nomeInvalido

  const enviar = async (e: FormEvent) => {
    e.preventDefault()
    setTocado(true)
    if (nomeInvalido) return

    setErro(null)
    setEnviando(true)
    try {
      await campanhaService.criar(nomeLimpo)
      navigate('/campanhas')
    } catch (err) {
      const texto = err instanceof Error ? err.message : ''
      // exceção do back em dev volta como página inteira: nesse caso, mensagem genérica
      setErro(texto && texto.length < 200 ? texto : 'Não foi possível criar a campanha.')
      setEnviando(false)
    }
  }

  return (
    <div className={styles.pagina}>
      <Link to="/campanhas" className={styles.voltar}>← Campanhas</Link>

      <form className={styles.card} onSubmit={enviar} noValidate>
        <h1 className={styles.titulo}>Nova campanha</h1>
        <p className={styles.subtitulo}>Dê um nome ao território que você vai mestrar</p>

        <label className={styles.campo}>
          <span className={styles.rotulo}>Nome da campanha</span>
          <input
            className={mostrarErroCampo ? styles.inputErro : styles.input}
            value={nome}
            onChange={e => setNome(e.target.value)}
            onBlur={() => setTocado(true)}
            maxLength={MAX}
            autoFocus
            autoComplete="off"
            placeholder="Ex.: A Queda de Valdoria"
          />
          <span className={styles.rodapeCampo}>
            {mostrarErroCampo
              ? <small className={styles.erroCampo}>O nome precisa ter pelo menos {MIN} caracteres.</small>
              : <span />}
            <small className={styles.contador}>{nome.length}/{MAX}</small>
          </span>
        </label>

        {erro && <p className={styles.erro}>{erro}</p>}

        <div className={styles.acoes}>
          <Link to="/campanhas" className={styles.cancelar}>Cancelar</Link>
          <button className={styles.botao} type="submit" disabled={enviando}>
            Criar campanha
          </button>
        </div>

        {enviando && (
          <div className={styles.carregando}>
            <CarregandoMapa texto="Traçando o território..." />
          </div>
        )}
      </form>
    </div>
  )
}