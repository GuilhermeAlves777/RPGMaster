import { useState, type ChangeEvent, type FormEvent } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'
import { useCarregar } from '../hooks/useCarregar'
import CarregandoMapa from '../components/CarregandoMapa'
import { campanhaService } from '../services/campanhaService'
import { catalogoService } from '../services/catalogoService'
import { personagemService } from '../services/personagemService'
import styles from './CriarPersonagem.module.css'

const vazio = {
  nome: '', nivel: '1', vidaMaxima: '', manaMaxima: '',
  idRaca: '', idClasse: '', jogador: '',
}

const num = (v: string) => (v.trim() === '' ? NaN : Number(v))

export default function CriarPersonagem() {
  const { idCampanha } = useParams()
  const id = Number(idCampanha)
  const navigate = useNavigate()
  const { usuario } = useAuth()
  const idUsuario = usuario?.id ?? -1

  const campanha = useCarregar(() => campanhaService.obter(id, idUsuario), [id, idUsuario])
  const racas = useCarregar(() => catalogoService.racasCompleto(id), [id])
  const classes = useCarregar(() => catalogoService.classesCompleto(id), [id])

  const [f, setF] = useState(vazio)
  const [erro, setErro] = useState<string | null>(null)
  const [enviando, setEnviando] = useState(false)

  const c = campanha.dados
  if (!Number.isFinite(id)) return <p>Campanha inválida.</p>
  if (campanha.erro) return <p className={styles.erro}>{campanha.erro}</p>
  if (!c) return <CarregandoMapa texto="Abrindo a campanha..." />

  const ehMestre = c.id_Criador === idUsuario
  const jogadores = c.jogadores ?? []
  const faltaCatalogo = !racas.loading && !classes.loading
    && ((racas.dados?.length ?? 0) === 0 || (classes.dados?.length ?? 0) === 0)

  const campo = (k: keyof typeof vazio) =>
    (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
      const v = e.target.value
      setF(prev => ({ ...prev, [k]: v }))
    }

  const validar = (): string | null => {
    if (!f.nome.trim()) return 'Informe o nome do personagem.'
    const nivel = num(f.nivel), vida = num(f.vidaMaxima), mana = num(f.manaMaxima)
    if (!Number.isInteger(nivel) || nivel < 1) return 'O nível precisa ser 1 ou mais.'
    if (!Number.isInteger(vida) || vida < 1) return 'A vida máxima precisa ser maior que 0.'
    if (!Number.isInteger(mana) || mana < 1) return 'A mana máxima precisa ser maior que 0.'
    if (!f.idRaca) return 'Escolha uma raça.'
    if (!f.idClasse) return 'Escolha uma classe.'
    if (ehMestre && !f.jogador) return 'Escolha o jogador dono do personagem ou marque como NPC.'
    return null
  }

  const enviar = async (e: FormEvent) => {
    e.preventDefault()
    const msg = validar()
    if (msg) { setErro(msg); return }

    setErro(null)
    setEnviando(true)
    try {
      await personagemService.criar({
        nome: f.nome.trim(),
        nivel: Number(f.nivel),
        idJogador: ehMestre ? (f.jogador === 'npc' ? 0 : Number(f.jogador)) : idUsuario,
        vidaMaxima: Number(f.vidaMaxima),
        manaMaxima: Number(f.manaMaxima),
        idCampanha: id,
        idRaca: Number(f.idRaca),
        idClasse: Number(f.idClasse),
        ehNpc: f.jogador == "npc" ? true : false
      })
      navigate(`/campanhas/${id}`)
    } catch (err) {
      const t = err instanceof Error ? err.message : ''
      setErro(t && t.length < 200 ? t : 'Não foi possível criar o personagem.')
      setEnviando(false)
    }
  }

  return (
    <div className={styles.pagina}>
      <Link to={`/campanhas/${id}`} className={styles.voltar}>← {c.nome}</Link>

      <form className={styles.card} onSubmit={enviar} noValidate>
        <h1 className={styles.titulo}>Novo personagem</h1>
        <p className={styles.subtitulo}>Dê forma a quem vai viver essa história</p>

        <label className={styles.campo}>
          <span className={styles.rotulo}>Nome</span>
          <input className={styles.input} value={f.nome} onChange={campo('nome')}
                 maxLength={100} autoFocus autoComplete="off" />
        </label>

        <div className={styles.duplo}>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Raça</span>
            <select className={styles.input} value={f.idRaca} onChange={campo('idRaca')}>
              <option value="">Selecione</option>
              {(racas.dados ?? []).map(r => (
                <option key={r.id_Raca} value={r.id_Raca}>{r.nome}</option>
              ))}
            </select>
          </label>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Classe</span>
            <select className={styles.input} value={f.idClasse} onChange={campo('idClasse')}>
              <option value="">Selecione</option>
              {(classes.dados ?? []).map(k => (
                <option key={k.id_Classe} value={k.id_Classe}>{k.nome}</option>
              ))}
            </select>
          </label>
        </div>

        {faltaCatalogo && (
          <p className={styles.aviso}>
            A campanha precisa ter pelo menos uma raça e uma classe cadastradas.{' '}
            <Link to={`/campanhas/${id}`}>Cadastrar agora</Link>
          </p>
        )}

        <div className={styles.triplo}>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Nível</span>
            <input className={styles.input} type="number" min={1} value={f.nivel} onChange={campo('nivel')} />
          </label>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Vida máxima</span>
            <input className={styles.input} type="number" min={1} value={f.vidaMaxima} onChange={campo('vidaMaxima')} />
          </label>
          <label className={styles.campo}>
            <span className={styles.rotulo}>Mana máxima</span>
            <input className={styles.input} type="number" min={1} value={f.manaMaxima} onChange={campo('manaMaxima')} />
          </label>
        </div>

        {ehMestre ? (
          <label className={styles.campo}>
            <span className={styles.rotulo}>Jogador</span>
            <select className={styles.input} value={f.jogador} onChange={campo('jogador')}>
              <option value="">Selecione</option>
              {jogadores.map(j => (
                <option key={j.id_Usuario} value={j.id_Usuario}>{j.nome}</option>
              ))}
              <option value="npc">NPC (sem jogador)</option>
            </select>
          </label>
        ) : (
          <p className={styles.fixo}>Este personagem será seu.</p>
        )}

        {erro && <p className={styles.erro}>{erro}</p>}

        <div className={styles.acoes}>
          <Link to={`/campanhas/${id}`} className={styles.cancelar}>Cancelar</Link>
          <button className={styles.botao} type="submit" disabled={enviando || faltaCatalogo}>
            Criar personagem
          </button>
        </div>

        {enviando && (
          <div className={styles.carregando}>
            <CarregandoMapa texto="Dando vida ao personagem..." />
          </div>
        )}
      </form>
    </div>
  )
}