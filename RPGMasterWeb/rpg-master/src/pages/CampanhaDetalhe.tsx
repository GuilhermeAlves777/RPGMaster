import { Link, useNavigate, useParams } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'
import { useCarregar } from '../hooks/useCarregar'
import { campanhaService } from '../services/campanhaService'
import { catalogoService } from '../services/catalogoService'
import Bloco from '../components/Bloco'
import BlocoCatalogo from '../components/BlocoCatalogo'
import PersonagemCard from '../components/PersonagemCard'
import ConviteCampanha from '../components/ConviteCampanha'
import styles from './CampanhaDetalhe.module.css'
import { useState } from 'react'
import ModalCriar from '../components/ModalCriar'
import { configCriar, type TipoCriar } from './campanhaCriar'

export default function CampanhaDetalhe() {
  const { idCampanha } = useParams()
  const id = Number(idCampanha)
  const { usuario } = useAuth()
  const idJogador = usuario?.id ?? 0
  const [criar, setCriar] = useState<TipoCriar | null>(null)
  
  const campanha = useCarregar(() => campanhaService.obter(id, idJogador), [id, idJogador])
  const racas = useCarregar(() => catalogoService.racas(id), [id])
  const atributos = useCarregar(() => catalogoService.atributos(id), [id])
  const pericias = useCarregar(() => catalogoService.pericias(id), [id])
  const itens = useCarregar(() => catalogoService.itens(id), [id])
  const classes = useCarregar(() => catalogoService.classes(id), [id])
  const magias = useCarregar(() => catalogoService.magias(id), [id])
  
  const navigate = useNavigate()   // junto dos outros hooks
  const ehJogador = !!campanha.dados?.jogadores?.some(j => j.id_Usuario === usuario?.id)

  const ehMestre = !!usuario && campanha.dados?.id_Criador === usuario.id

  const recarregar: Record<TipoCriar, () => void> = {
    raca: racas.recarregar,
    classe: classes.recarregar,
    atributo: atributos.recarregar,
    pericia: pericias.recarregar,
    item: itens.recarregar,
    magia: magias.recarregar,
  }

  const cfg = criar ? configCriar(criar, id) : null

  if (!Number.isFinite(id)) return <p>Campanha inválida.</p>

  // TODO: abrir modal/formulário de cadastro de cada tipo
  const adicionar = (o: string) => () => console.log('adicionar', o)

  return (
    <div className={styles.pagina}>
      <Link to="/campanhas" className={styles.voltar}>
        ← Campanhas
      </Link>
      <h1 className={styles.titulo}>{campanha.dados?.nome ?? 'Campanha'}</h1>

      {ehMestre && (
        <Bloco titulo="Convite" podeAdicionar={false} onAdicionar={() => {}}
               vazio={false} textoVazio="">
          <ConviteCampanha idCampanha={id} />
        </Bloco>
      )}

      <Bloco
          titulo="Personagens"
          podeAdicionar={ehMestre || ehJogador}
          onAdicionar={() => navigate(`/campanhas/${id}/personagens/novo`)}
          loading={campanha.loading}
          erro={campanha.erro}
          vazio={campanha.dados?.personagens.length === 0}
          textoVazio="Nenhum personagem nesta campanha."
        >
          <div className={styles.faixa}>
            {campanha.dados?.personagens.map((p) => (
              <PersonagemCard key={p.id_Personagem} p={p} idCampanha={id} />
            ))}
          </div>
      </Bloco>

      <div className={styles.grade}>
        <BlocoCatalogo titulo="Raças" estado={racas} podeAdicionar={ehMestre} onAdicionar={() => setCriar('raca')} />
        <BlocoCatalogo titulo="Atributos" estado={atributos} podeAdicionar={ehMestre} onAdicionar={() => setCriar('atributo')} />
        <BlocoCatalogo titulo="Perícias" estado={pericias} podeAdicionar={ehMestre} onAdicionar={() => setCriar('pericia')} />
        <BlocoCatalogo titulo="Itens" estado={itens} podeAdicionar={ehMestre} onAdicionar={() => setCriar('item')} />
        <BlocoCatalogo titulo="Classes" estado={classes} podeAdicionar={ehMestre} onAdicionar={() => setCriar('classe')} />
        <BlocoCatalogo titulo="Magias" estado={magias} podeAdicionar={ehMestre} onAdicionar={() => setCriar('magia')} />
      </div>

      {cfg && criar && (
        <ModalCriar
          titulo={cfg.titulo}
          campos={cfg.campos}
          onConfirmar={async v => {
            await cfg.salvar(v)
            recarregar[criar]()
          }}
          onFechar={() => setCriar(null)}
        />
      )}
    </div>
  )
}