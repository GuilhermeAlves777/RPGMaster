import { api } from './api'
import type { CampanhaDto } from '../types'

const listarPorJogador = (idJogador: number) =>
  api.get<CampanhaDto[]>(`/Campanha/ObterPorJogador?idJogador=${idJogador}`)

export const campanhaService = {
  listarPorJogador,
  obter: async (idCampanha: number, idJogador: number) => {
    const lista = await listarPorJogador(idJogador)
    const campanha = lista.find((c) => c.id_Campanha === idCampanha)
    if (!campanha) throw new Error('Campanha não encontrada')
    return campanha
  },

  criar: (nome: string) =>
    api.post(`/Campanha/Criar?nome=${encodeURIComponent(nome)}`, undefined),
}