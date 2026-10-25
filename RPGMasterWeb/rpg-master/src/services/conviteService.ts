import { api } from './api'
import type { ConviteDto }  from '../types'

export const conviteService = {
  gerar: (idCampanha: number) =>
    api.post<string>(`/Convite/GerarConvite?idCampanha=${idCampanha}`, undefined),

  // AJUSTA: rota do seu ObterConvite. Se não houver convite, o ideal é o back devolver
  // 204 (corpo vazio), e o front trata como "ainda não existe".
  obter: (idCampanha: number) =>
    api.get<string | undefined>(`/Convite/ObterAtivo?idCampanha=${idCampanha}`),

  consultar: (codigo: string) =>
  api.get<ConviteDto | undefined>(`/Convite/ObterConvite?Codigo=${encodeURIComponent(codigo)}`),

  aceitar: (codigo: string) =>
    api.post<string>(`/Convite/AceiteConvite?Codigo=${encodeURIComponent(codigo)}`, undefined),
}