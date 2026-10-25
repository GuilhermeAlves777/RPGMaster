export interface LoginResponse {
  token: string
}

export interface PersonagemDto {
  id_Personagem: number
  id_Campanha: number
  nome: string
  nivel: number
  ehNpc: boolean
  // ainda não vêm da API; o front já está pronto pra quando vierem
  nomeJogador?: string
  vidaAtual?: number
  vidaMaxima?: number
  fotoUrl?: string
}

export interface Usuario {
  id: number
  nome: string
  fotoUrl?: string
}

// Ajuste ao JSON real do Swagger
export interface CampanhaDto {
  id_Campanha: number
  nome: string
  id_Criador: number
  personagens: PersonagemDto[]
  jogadores: JogadorDto[]
}

export interface JogadorDto {
  id_Usuario: number
  nome: string
}

export interface CatalogoDto {
  id: number
  nome: string
  descricao?: string
}

export interface AtributoCatalogoDto {
  id_Atributo: number
  nome: string
  valor_Padrao: number
  id_Campanha: number
}

export interface PericiaCatalogoDto {
  id_Pericia: number
  nome: string
  valor_Padrao: number
  id_Campanha: number
}

export interface MagiaCatalogoDto {
  id_Magia: number
  nome: string
  dados: string
  id_Campanha: number
}

export interface ItemCatalogoDto {
  id_Item: number
  nome: string
  tipo: string | number   // AJUSTA: você trocou Tipo por enum, confere se vem número ou texto
  descricao?: string | null
  imagem?: string | null
  id_Campanha: number
}

export interface PersonagemDetalheDto {
  id_Personagem: number
  id_Campanha: number
  nome: string
  id_Jogador: number | null    
  nomeJogador?: string | null
  imagem: string | null
  nivel: number
  ehNpc: boolean
  vidaAtual: number
  vidaMaxima: number
  manaAtual: number
  manaMaxima: number
  classe: { id_Classe: number; nome: string }
  itens?: PersonagemItemDto[]
  atributos: { id_Atributo: number; nome: string; valor: number }[]
  pericias: { id_Pericia: number; nome: string; valor: number }[]   // AJUSTA: adicionar no back
  magias: { id_Magia: number; nome: string; dados?: string }[]      // AJUSTA: adicionar no back
}

export interface PersonagemItemDto {
  id_Item: number
  nome: string
  imagem?: string | null
  quantidade: number
}

export interface ConviteDto {
  id_Campanha: number
  id_Convite: number
  campanha: string   // nome da campanha
}

export interface RacaCatalogoDto { id_Raca: number; nome: string }       // AJUSTA
export interface ClasseCatalogoDto { id_Classe: number; nome: string }   // AJUSTA