import { api } from './api'
import type { PersonagemDto, PersonagemDetalheDto } from '../types'

export const personagemService = {
  listarPorCampanha: (idCampanha: number) =>
    api.get<PersonagemDto[]>(`/Personagem/ObterTodos?idCampanha=${idCampanha}`),

  detalhe: (idCampanha: number, idPersonagem: number) =>
    api.get<PersonagemDetalheDto>(`/Personagem/ObterPorId?idCampanha=${idCampanha}&idPersonagem=${idPersonagem}`),   // AJUSTA: sua rota

  editarAtributo: (idPersonagem: number, idCampanha: number, idAtributo: number, valor: number) =>
    api.put(`/PersonagemAtributo/Editar`, { idPersonagem, idCampanha, idAtributo, valor }),

  editarPericia: (idPersonagem: number, idCampanha: number, idPericia: number, valor: number) =>
    api.put(`/PersonagemPericia/Editar`, { idPersonagem, idCampanha, idPericia, valor }),

  alterarVida: (idPersonagem: number, idCampanha: number, vidaAtual: number) =>
    api.put(`/Personagem/AlterarVida`, { idPersonagem, idCampanha, vidaAtual }), 
  
  alterarMana: (idPersonagem: number, idCampanha: number, manaAtual: number) =>
    api.put(`/Personagem/AlterarMana`, { idPersonagem, idCampanha, manaAtual }),

  editarFicha: (
  idPersonagem: number,
  idCampanha: number,
  nivel?: number,
  vidaMaxima?: number,
  manaMaxima?: number,
) =>
  api.put(`/Personagem/EditarPersonagem`, {
    idPersonagem,
    idCampanha,
    nivel,
    vidaMaxima,
    manaMaxima,
  }),   

  adicionarItem: (idPersonagem: number, idCampanha: number, idItem: number, quantidade: number) =>
    api.post(`/PersonagemItem/Adicionar`, { idPersonagem, idCampanha, idItem, quantidade }),   // AJUSTA: rota e campos do seu Controller

  editarItem: (idPersonagem: number, idItem: number, quant: number) =>
    api.put(`/PersonagemItem/Editar`, { idPersonagem, idItem, quant }),   // AJUSTA: rota e campos do seu Controller

  adicionarAtributo: (idPersonagem: number, idCampanha: number, idAtributo: number, valor: number) =>
    api.post(`/PersonagemAtributo/PersonagemAtributo/Adicionar?idPersonagem=${idPersonagem}&idAtributo=${idAtributo}&idCampanha=${idCampanha}&valor=${valor}`, undefined),
  
  adicionarPericia: (idPersonagem: number, idCampanha: number, idPericia: number, valor: number) =>
    api.post(`/PersonagemPericia/Adicionar`, { idPersonagem, idPericia, idCampanha, valor }),
  
  adicionarMagia: (idPersonagem: number, idCampanha: number, idMagia: number) =>
    api.post(`/PersonagemMagia/Adicionar?idCampanha=${idCampanha}&idPersonagem=${idPersonagem}&idMagia=${idMagia}`, undefined),

  excluirAtributo: (idPersonagem: number, idCampanha: number, idAtributo: number) =>
    api.delete(`/PersonagemAtributo/PersonagemAtributo/Excluir?idPersonagem=${idPersonagem}&idAtributo=${idAtributo}&idCampanha=${idCampanha}`),
  
  excluirPericia: (idPersonagem: number, idPericia: number) =>
    api.delete(`/PersonagemPericia/${idPersonagem}/${idPericia}`),

  excluirMagia: (idPersonagem: number, idMagia: number) =>
    api.delete(`/PersonagemMagia/Excluir?idPersonsagem=${idPersonagem}&idMagia=${idMagia}`),

  excluirItem: (idPersonagem: number, idItem: number) =>
    api.delete(`/PersonagemItem/Excluir?idPersonagem=${idPersonagem}&idItem=${idItem}`),

  criar: (d: {
    nome: string; nivel: number; idJogador?: number; vidaMaxima: number
    manaMaxima: number; idCampanha: number; idRaca: number; idClasse: number
    ehNpc: boolean
  }) =>
    api.post('/Personagem/Criar', {   // AJUSTA: confere se a rota continua "Criar"
      nome: d.nome,
      nivel: d.nivel,
      idJogador: d.idJogador,
      vidaMaxima: d.vidaMaxima,
      manaMaxima: d.manaMaxima,
      idCampanha: d.idCampanha,
      idRaca: d.idRaca,
      idClasse: d.idClasse,
      ehNPC: d.ehNpc,
    }),
}

