import { useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'
import { useCarregar } from '../hooks/useCarregar'
import { personagemService } from '../services/personagemService'
import { campanhaService } from '../services/campanhaService'
import Bloco from '../components/Bloco'
import { ControleValor } from '../components/ControleValorProps'
import styles from './PersonagemDetalhe.module.css'
import type { PersonagemItemDto } from '../types'
import ModalAdicionar, { type Opcao } from '../components/ModalAdicionar'
import { catalogoService } from '../services/catalogoService'

function Barra({ rotulo, atual, maximo, tipo, podeEditar, onSalvar }: {
  rotulo: string
  atual: number
  maximo: number
  tipo: 'vida' | 'mana'
  podeEditar?: boolean
  onSalvar?: (novo: number) => Promise<void>
}) {
  const [editando, setEditando] = useState(false)
  const [valor, setValor] = useState(atual)
  const [erro, setErro] = useState<string | null>(null)
  const [modal, setModal] = useState<'atributo' | 'pericia' | 'magia' | 'item' | null>(null)

  const pct = maximo > 0 ? Math.min(100, Math.max(0, (atual / maximo) * 100)) : 0
  const cor = tipo === 'mana' ? styles.mana : pct < 30 ? styles.vidaBaixa : styles.vida

  const abrir = () => { setValor(atual); setErro(null); setEditando(true) }
  const cancelar = () => setEditando(false)

  const confirmar = async () => {
    if (!onSalvar) return
    const limitado = Math.min(maximo, Math.max(0, Math.round(valor))) // só conforto: o back valida de verdade
    try {
      await onSalvar(limitado)
      setEditando(false)
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Não foi possível salvar')
    }
  }

  return (
    <div className={styles.barraBloco}>
      <div className={styles.barraTopo}>
        <span>{rotulo}</span>

        {editando ? (
          <span className={styles.edicao}>
            <input
              className={styles.campoEdicao}
              type="number"
              min={0}
              max={maximo}
              value={valor}
              autoFocus
              onChange={e => setValor(Number(e.target.value))}
              onKeyDown={e => {
                if (e.key === 'Enter') confirmar()
                if (e.key === 'Escape') cancelar()
              }}
            />
            <span>/ {maximo}</span>
            <button className={styles.btnMini} onClick={confirmar} title="Salvar">✓</button>
            <button className={styles.btnMini} onClick={cancelar} title="Cancelar">✕</button>
          </span>
        ) : (
          <span className={styles.edicao}>
            {atual} / {maximo}
            {podeEditar && onSalvar && (
              <button className={styles.btnMini} onClick={abrir} title="Editar">✎</button>
            )}
          </span>
        )}
      </div>

      <div className={styles.barra}>
        <div className={cor} style={{ width: `${pct}%` }} />
      </div>
      {erro && <small className={styles.erro}>{erro}</small>}
    </div>
  )
}

function CampoEditavel({ rotulo, valor, podeEditar, onSalvar }: {
  rotulo: string
  valor: number
  podeEditar: boolean
  onSalvar: (novo: number) => Promise<void>
}) {
  const [editando, setEditando] = useState(false)
  const [novo, setNovo] = useState(valor)
  const [erro, setErro] = useState<string | null>(null)

  const abrir = () => { setNovo(valor); setErro(null); setEditando(true) }

  const confirmar = async () => {
    if (!Number.isFinite(novo) || novo <= 0) {
      setErro('O valor precisa ser maior que 0.')
      return
    }
    try {
      await onSalvar(Math.round(novo))
      setEditando(false)
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Não foi possível salvar')
    }
  }

  return (
    <div className={styles.campoBloco}>
      <div className={styles.campoLinha}>
        <span className={styles.campoRotulo}>{rotulo}</span>

        {editando ? (
          <span className={styles.edicao}>
            <input
              className={styles.campoEdicao}
              type="number"
              min={1}
              value={novo}
              autoFocus
              onChange={e => setNovo(Number(e.target.value))}
              onKeyDown={e => {
                if (e.key === 'Enter') confirmar()
                if (e.key === 'Escape') setEditando(false)
              }}
            />
            <button className={styles.btnMini} onClick={confirmar} title="Salvar">✓</button>
            <button className={styles.btnMini} onClick={() => setEditando(false)} title="Cancelar">✕</button>
          </span>
        ) : (
          <span className={styles.edicao}>
            <strong>{valor}</strong>
            {podeEditar && (
              <button className={styles.btnMini} onClick={abrir} title="Editar">✎</button>
            )}
          </span>
        )}
      </div>
      {erro && <small className={styles.erro}>{erro}</small>}
    </div>
  )
}

function Inventario({ itens, podeEditar, onAlterar }: {
  itens: PersonagemItemDto[]
  podeEditar: boolean
  onAlterar: (idItem: number, novaQuantidade: number) => Promise<void>
}) {
  return (
    <div className={styles.inventario}>
      {itens.map(i => (
        <div key={i.id_Item} className={styles.slot} title={i.nome}>
          <div className={styles.slotImagem}>
            {i.imagem ? (
              <img src={i.imagem} alt={i.nome} />
            ) : (
              <span aria-hidden="true">{i.nome.charAt(0).toUpperCase()}</span>
            )}
            <span className={styles.slotQtd}>{i.quantidade}</span>
          </div>

          <span className={styles.slotNome}>{i.nome}</span>

          {podeEditar && (
            <div className={styles.slotAcoes}>
              <button className={styles.btnMini} onClick={() => onAlterar(i.id_Item, i.quantidade - 1)}
                title={i.quantidade <= 1 ? 'Remover' : 'Diminuir'}>−</button>
              <button className={styles.btnMini} onClick={() => onAlterar(i.id_Item, i.quantidade + 1)}
                title="Aumentar">+</button>
            </div>
          )}
        </div>
      ))}
    </div>
  )
}

export function PersonagemDetalhePage() {
  const { idCampanha, idPersonagem } = useParams()
  const idC = Number(idCampanha)
  const idP = Number(idPersonagem)
  const { usuario } = useAuth()
  const idUsuarioLogado = usuario?.id ?? -1
  const [modal, setModal] = useState<'atributo' | 'pericia' | 'magia' | 'item' | null>(null)

  const personagem = useCarregar(() => personagemService.detalhe(idC, idP), [idC, idP])
  const campanha = useCarregar(() => campanhaService.obter(idC, idUsuarioLogado), [idC, idUsuarioLogado])

  const p = personagem.dados
  if (personagem.erro) return <p className={styles.erro}>{personagem.erro}</p>
  if (!p) return <p>Carregando...</p>

  const ehMestre = campanha.dados?.id_Criador === idUsuarioLogado
  const ehDono = p.id_Jogador === idUsuarioLogado
  const podeEditar = ehMestre || ehDono

  const alterarAtributo = async (idAtributo: number, novo: number) => {
    await personagemService.editarAtributo(idP, idC, idAtributo, novo)
    personagem.recarregar()
  }

  const alterarPericia = async (idPericia: number, novo: number) => {
    await personagemService.editarPericia(idP, idC, idPericia, novo)
    personagem.recarregar()
  }

  const alterarVida = async (novo: number) => {
    await personagemService.alterarVida(idP, idC, novo)
    personagem.recarregar()
  }

  const alterarMana = async (novo: number) => {
    await personagemService.alterarMana(idP, idC, novo)
    personagem.recarregar()
}

  const editarFicha = async (nivel?: number, vidaMaxima?: number, manaMaxima?: number) => {
    await personagemService.editarFicha(idP, idC, nivel, vidaMaxima, manaMaxima)
    personagem.recarregar()
  }

  const alterarItem = async (idItem: number, quantidade: number) => {
    await personagemService.editarItem(idP, idItem, quantidade)
    personagem.recarregar()
  }

  const adicionarAtributo = async (idAtributo: number, valor: number) => {
    await personagemService.adicionarAtributo(idP, idC, idAtributo, valor)
  personagem.recarregar()
  }

  const adicionarPericia = async (idPericia: number, valor: number) => {
    await personagemService.adicionarPericia(idP, idC, idPericia, valor)
    personagem.recarregar()
  }
  
  const adicionarMagia = async (idMagia: number) => {
    await personagemService.adicionarMagia(idP, idC, idMagia)
    personagem.recarregar()
  }

  const adicionarItem = async (idItem: number, quantidade: number) => {
    await personagemService.adicionarItem(idP, idC, idItem, quantidade)
    personagem.recarregar()
  }

  const excluirAtributo = async (idAtributo: number, nome: string) => {
  if (!window.confirm(`Remover "${nome}" deste personagem?`)) return
  await personagemService.excluirAtributo(idP, idC, idAtributo)
  personagem.recarregar()
  }

  const excluirPericia = async (idPericia: number, nome: string) => {
    if (!window.confirm(`Remover "${nome}" deste personagem?`)) return
    await personagemService.excluirPericia(idP, idPericia)
    personagem.recarregar()
  }

  const excluirMagia = async (idMagia: number, nome: string) => {
    if (!window.confirm(`Remover "${nome}" deste personagem?`)) return
    await personagemService.excluirMagia(idP, idMagia)
    personagem.recarregar()
  }

  return (
    <div className={styles.pagina}>
      <Link to={`/campanhas/${idC}`} className={styles.voltar}>← Campanha</Link>

      <div className={styles.layout}>
        <aside className={styles.geral}>
          {p.imagem ? (
            <img className={styles.foto} src={p.imagem} alt={p.nome} />
          ) : (
            <div className={styles.foto} aria-hidden="true">
              {p.nome.charAt(0).toUpperCase()}
            </div>
          )}


          <h1 className={styles.nome}>{p.nome}</h1>
          <p className={styles.jogador}>{p.ehNpc ? 'NPC' : (p.nomeJogador ?? 'Jogador')}</p>
          <div className={styles.campoBloco}>
          <div className={styles.campoLinha}>
            <span className={styles.campoRotulo}>Classe</span>
            <span className={styles.campoTexto}>{p.classe?.nome ?? '—'}</span>
          </div>
        </div>

          <CampoEditavel rotulo="Nível" valor={p.nivel} podeEditar={podeEditar}
                         onSalvar={v => editarFicha(v, undefined, undefined)} />
          <CampoEditavel rotulo="Vida máxima" valor={p.vidaMaxima} podeEditar={podeEditar}
                         onSalvar={v => editarFicha(undefined, v, undefined)} />
          <CampoEditavel rotulo="Mana máxima" valor={p.manaMaxima} podeEditar={podeEditar}
                         onSalvar={v => editarFicha(undefined, undefined, v)} />

          <Barra rotulo="Vida" atual={p.vidaAtual} maximo={p.vidaMaxima} tipo="vida"
                 podeEditar={podeEditar} onSalvar={alterarVida} />
          <Barra rotulo="Mana" atual={p.manaAtual} maximo={p.manaMaxima} tipo="mana"
                 podeEditar={podeEditar} onSalvar={alterarMana} />
        </aside>

        <main className={styles.mutaveis}>
          <Bloco
            titulo="Atributos"
            podeAdicionar={podeEditar}
            onAdicionar={() => setModal('atributo')}  
            vazio={(p.atributos?.length ?? 0) === 0}
            textoVazio="Nenhum atributo."
          >
            {p.atributos?.map(a => (
              <div key={a.id_Atributo} className={styles.linha}>
                <span>{a.nome}</span>
                <ControleValor valor={a.valor} podeEditar={podeEditar}
                               onChange={v => alterarAtributo(a.id_Atributo, v)}
                               onExcluir={() => excluirAtributo(a.id_Atributo, a.nome)}/>
                               
              </div>
            ))}
          </Bloco>

          <Bloco
            titulo="Perícias"
            podeAdicionar={podeEditar}
            onAdicionar={() => setModal('pericia')}   // 'pericia' e 'magia' nos outros dois
            vazio={(p.pericias?.length ?? 0) === 0}
            textoVazio="Nenhuma perícia."
          >
            {p.pericias?.map(x => (
              <div key={x.id_Pericia} className={styles.linha}>
                <span>{x.nome}</span>
                <ControleValor valor={x.valor} podeEditar={podeEditar}
                               onChange={v => alterarPericia(x.id_Pericia, v)} 
                               onExcluir={() => excluirPericia(x.id_Pericia, x.nome)}/>
              </div>
            ))}
          </Bloco>

          <Bloco
            titulo="Magias"
            podeAdicionar={podeEditar}
            onAdicionar={() => setModal('magia')}  
            vazio={(p.magias?.length ?? 0) === 0}
            textoVazio="Nenhuma magia."
          >
            {p.magias?.map(m => (
              <div key={m.id_Magia} className={styles.linha}>
                <span>{m.nome}</span>
                {m.dados && <small className={styles.info}>{m.dados}</small>}
                {podeEditar && (
                  <button className={styles.btnMini} onClick={() => excluirMagia(m.id_Magia, m.nome)} title="Excluir">✕</button>
                )}
              </div>
            ))}
          </Bloco>
          <div className={styles.larguraTotal}>
          <Bloco
            titulo="Inventário"
            podeAdicionar={podeEditar}
            onAdicionar={() => setModal('item')}
            vazio={(p.itens?.length ?? 0) === 0}
            textoVazio="Inventário vazio."
          >
            <Inventario itens={p.itens ?? []} podeEditar={podeEditar} onAlterar={alterarItem} />
          </Bloco>
        </div>
        </main>
      </div>
      {modal === 'atributo' && (
      <ModalAdicionar
        titulo="Adicionar atributo"
        carregar={async () => (await catalogoService.atributosCompleto(idC)).map(x => ({
          id: x.id_Atributo, nome: x.nome, valorPadrao: x.valor_Padrao,
        }))}
        jaPossui={(p.atributos ?? []).map(a => a.id_Atributo)}
        pedeValor
        onConfirmar={adicionarAtributo}
        onFechar={() => setModal(null)}
      />
    )}

    {modal === 'pericia' && (
      <ModalAdicionar
        titulo="Adicionar perícia"
        carregar={async () => (await catalogoService.periciasCompleto(idC)).map(x => ({
          id: x.id_Pericia, nome: x.nome, valorPadrao: x.valor_Padrao,
        }))}

        jaPossui={(p.pericias ?? []).map(x => x.id_Pericia)}
        pedeValor
        onConfirmar={adicionarPericia}
        onFechar={() => setModal(null)}
      />
    )}

    {modal === 'magia' && (
      <ModalAdicionar
        titulo="Adicionar magia"
        carregar={async () => (await catalogoService.magiasCompleto(idC)).map(x => ({
          id: x.id_Magia, nome: x.nome,
        }))}
        jaPossui={(p.magias ?? []).map(m => m.id_Magia)}
        pedeValor={false}
        onConfirmar={id => adicionarMagia(id)}
        onFechar={() => setModal(null)}
      />
    )}

    {modal === 'item' && (
      <ModalAdicionar
        titulo="Adicionar item"
        rotuloValor="Quantidade"
        carregar={async () => (await catalogoService.itensCompleto(idC)).map(x => ({
          id: x.id_Item, nome: x.nome, valorPadrao: 1,
        }))}
        jaPossui={(p.itens ?? []).map(i => i.id_Item)}
        pedeValor
        onConfirmar={adicionarItem}
        onFechar={() => setModal(null)}
      />
    )}
    </div>
  )
}