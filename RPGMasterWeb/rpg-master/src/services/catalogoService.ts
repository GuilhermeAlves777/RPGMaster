import { api } from './api'
import type {
  CatalogoDto,
  AtributoCatalogoDto,
  PericiaCatalogoDto,
  MagiaCatalogoDto,
  ItemCatalogoDto,
  RacaCatalogoDto,
  ClasseCatalogoDto
} from '../types'

// Versões completas: o modal da ficha usa essas
const atributosCompleto = (idCampanha: number) =>
  api.get<AtributoCatalogoDto[]>(`/Atributo/ObterPorCampanha?idCampanha=${idCampanha}`)

const periciasCompleto = (idCampanha: number) =>
  api.get<PericiaCatalogoDto[]>(`/Pericia/ObterPorCampanha?idCampanha=${idCampanha}`)

const itensCompleto = (idCampanha: number) =>
  api.get<ItemCatalogoDto[]>(`/Item/ObterPorCampanha?idCampanha=${idCampanha}`)

const magiasCompleto = (idCampanha: number) =>
  api.get<MagiaCatalogoDto[]>(`/Magia/ObterPorCampanha?idCampanha=${idCampanha}`)

export const catalogoService = {
  // Formato que o BlocoCatalogo espera: { id, nome }
  racasCompleto: (idCampanha: number) =>
    api.get<RacaCatalogoDto[]>(`/${encodeURIComponent('Raça')}/ObterTodos?idCampanha=${idCampanha}`),

  classesCompleto: (idCampanha: number) =>
    api.get<ClasseCatalogoDto[]>(`/Classe/ObterTodos?id=${idCampanha}`),
  
  racas: (idCampanha: number) =>
    api.get<CatalogoDto[]>(`/${encodeURIComponent('Raça')}/ObterTodos?idCampanha=${idCampanha}`),

  classes: (idCampanha: number) =>
    api.get<CatalogoDto[]>(`/Classe/ObterTodos?id=${idCampanha}`),

  atributos: async (idCampanha: number): Promise<CatalogoDto[]> =>
    (await atributosCompleto(idCampanha)).map(a => ({ id: a.id_Atributo, 
      nome: a.nome, valorPadrao: a.valor_Padrao })),

  pericias: async (idCampanha: number): Promise<CatalogoDto[]> =>
    (await periciasCompleto(idCampanha)).map(p => ({ id: p.id_Pericia, nome: p.nome })),

  itens: async (idCampanha: number): Promise<CatalogoDto[]> =>
    (await itensCompleto(idCampanha)).map(i => ({ id: i.id_Item, nome: i.nome })),

  magias: async (idCampanha: number): Promise<CatalogoDto[]> =>
    (await magiasCompleto(idCampanha)).map(m => ({ id: m.id_Magia, nome: m.nome })),

  atributosCompleto,
  periciasCompleto,
  itensCompleto,
  magiasCompleto,
}