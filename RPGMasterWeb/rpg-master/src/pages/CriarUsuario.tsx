import { useState, type ChangeEvent, type FormEvent } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { usuarioService } from '../services/usuarioService'
import styles from './CriarUsuario.module.css'
import CarregandoMapa from '../components/CarregandoMapa'

const vazio = { nome: '', sobrenome: '', user: '', email: '', cpf: '', dn: '', senha: '', confirmar: '' }

function formatarCpf(valor: string): string {
  const d = valor.replace(/\D/g, '').slice(0, 11)
  if (d.length <= 3) return d
  if (d.length <= 6) return `${d.slice(0, 3)}.${d.slice(3)}`
  if (d.length <= 9) return `${d.slice(0, 3)}.${d.slice(3, 6)}.${d.slice(6)}`
  return `${d.slice(0, 3)}.${d.slice(3, 6)}.${d.slice(6, 9)}-${d.slice(9)}`
}

export default function CriarUsuario() {
  const navigate = useNavigate()
  const [f, setF] = useState(vazio)
  const [erro, setErro] = useState<string | null>(null)
  const [enviando, setEnviando] = useState(false)
  const [ok, setOk] = useState(false)

  const [cpfTocado, setCpfTocado] = useState(false)

  const cpfDigitos = f.cpf.replace(/\D/g, '')
  const cpfIncompleto = cpfDigitos.length !== 11

  const campo = (k: keyof typeof vazio) => (e: ChangeEvent<HTMLInputElement>) => {
    const v = e.target.value
    setF(prev => ({ ...prev, [k]: v }))
  }

  const validar = (): string | null => {
    if (!f.nome.trim() || !f.sobrenome.trim() || !f.user.trim() || !f.email.trim()
        || !f.cpf.trim() || !f.dn || !f.senha) return 'Preencha todos os campos.'
    if (!/^\S+@\S+\.\S+$/.test(f.email)) return 'E-mail inválido.'
    if (f.cpf.replace(/\D/g, '').length !== 11) return 'O CPF precisa ter 11 dígitos.'
    if (new Date(f.dn) > new Date()) return 'A data de nascimento não pode ser no futuro.'
    if (f.senha.length < 6) return 'A senha precisa ter pelo menos 6 caracteres.'   // AJUSTA: sua regra
    if (f.senha !== f.confirmar) return 'As senhas não conferem.'
    if (cpfIncompleto) return 'CPF incompleto. Use o formato 000.000.000-00.'
    return null
  }

  const alterarCpf = (e: ChangeEvent<HTMLInputElement>) => {
        setF(prev => ({ ...prev, cpf: formatarCpf(e.target.value) }))
  }   

  const enviar = async (e: FormEvent) => {
    e.preventDefault()
    const msg = validar()
    if (msg) { setErro(msg); setCpfTocado(true); return }

    setErro(null)
    setEnviando(true)
    try {
      await usuarioService.criar({
        nome: f.nome.trim(),
        sobrenome: f.sobrenome.trim(),
        user: f.user.trim(),
        senha: f.senha,
        email: f.email.trim(),
        cpf: f.cpf.replace(/\D/g, ''),     // AJUSTA: se o back guarda com máscara, mande como digitado
        dn: `${f.dn}T00:00:00`,
      })
      setOk(true)
      setTimeout(() => navigate('/login'), 1200)
    } catch (err) {
      const texto = err instanceof Error ? err.message : ''
      // exceção do back em dev volta como página inteira: nesse caso mostra mensagem genérica
      setErro(texto && texto.length < 200 ? texto : 'Não foi possível criar a conta.')
    } finally {
      setEnviando(false)
    }
  }

  return (
    <div className={styles.pagina}>
      <form className={styles.card} onSubmit={enviar} noValidate>
        <h1 className={styles.titulo}>RPGMaster</h1>
        <p className={styles.subtitulo}>Crie sua conta e comece sua jornada</p>

        <div className={styles.duplo}>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Nome</span>
            <input className={styles.input} value={f.nome} onChange={campo('nome')} autoComplete="given-name" />
          </label>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Sobrenome</span>
            <input className={styles.input} value={f.sobrenome} onChange={campo('sobrenome')} autoComplete="family-name" />
          </label>
        </div>

        <label className={styles.campo}>
          <span className={styles.rotulo}>Usuário</span>
          <input className={styles.input} value={f.user} onChange={campo('user')} autoComplete="username" />
        </label>

        <label className={styles.campo}>
          <span className={styles.rotulo}>E-mail</span>
          <input className={styles.input} type="email" value={f.email} onChange={campo('email')} autoComplete="email" />
        </label>

        <div className={styles.duplo}>
          <label className={styles.campo}>
            <span className={styles.rotulo}>CPF</span>
            <input
              className={cpfTocado && cpfIncompleto ? styles.inputErro : styles.input}
              value={f.cpf}
              onChange={alterarCpf}
              onBlur={() => setCpfTocado(true)}
              inputMode="numeric"
              placeholder="000.000.000-00"
              maxLength={14}
              autoComplete="off"
            />
            {cpfTocado && cpfIncompleto && (
              <small className={styles.erroCampo}>
                {cpfDigitos.length === 0 ? 'Informe o CPF.' : 'O CPF precisa ter 11 dígitos.'}
              </small>
            )}
          </label>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Nascimento</span>
            <input className={styles.input} type="date" value={f.dn} onChange={campo('dn')} />
          </label>
        </div>

        <div className={styles.duplo}>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Senha</span>
            <input className={styles.input} type="password" value={f.senha} onChange={campo('senha')} autoComplete="new-password" />
          </label>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Confirmar senha</span>
            <input className={styles.input} type="password" value={f.confirmar} onChange={campo('confirmar')} autoComplete="new-password" />
          </label>
        </div>

        {erro && <p className={styles.erro}>{erro}</p>}
        {ok && <p className={styles.sucesso}>Conta criada! Redirecionando para o login...</p>}

        <button className={styles.botao} type="submit" disabled={enviando || ok}>
          {enviando && (
              <div className={styles.carregando}>
                <CarregandoMapa texto="Criando sua conta..." />
              </div>
            )}
            Criar Conta</button>

        <p className={styles.rodape}>
          Já tem conta? <Link to="/login">Entrar</Link>
        </p>
      </form>
    </div>
  )
}