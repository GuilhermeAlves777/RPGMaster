import { api } from './api'

function comQuery(base: string, dados: Record<string, string | number>) {
  const q = new URLSearchParams()
  for (const [k, v] of Object.entries(dados)) q.set(k, String(v))
  return `${base}?${q.toString()}`
}

export const criacaoService = {
  raca: (idCampanha: number, nome: string, descricao: string) =>
    api.post(comQuery(`/${encodeURIComponent('Raça')}/Criar`, { idCampanha, nome, descricao }), undefined),

  // AJUSTA: rota e formato do Classe (aqui supus igual à Raça)
  classe: (idCampanha: number, nome: string, descricao: string) =>
    api.post(comQuery('/Classe/Criar', { idCampanha, nome, descricao }), undefined),

  // AJUSTA: rota e formato do Atributo
  atributo: (idCampanha: number, nome: string, valorPadrao: number) =>
    api.post('/Atributo/Adicionar', { nome, valorPadrao, idCampanha }),

  pericia: (idCampanha: number, nome: string, valorPadrao: number) =>
    api.post('/Pericia/Cadastrar', { nome, valorPadrao, idCampanha }),

  magia: (idCampanha: number, nome: string, dados: string) =>
    api.post('/Magia/Adicionar', { nome, dados, idCampanha }),

  item: (idCampanha: number, nome: string, tipo: number, descricao: string, imagem: string) =>
    api.post('/Item/Adicionar', { nome, tipo, descricao, imagem, idCampanha }),
}