import { api } from './api'

export interface CriarUsuarioDados {
  nome: string
  sobrenome: string
  user: string
  senha: string
  email: string
  cpf: string
  dn: string   // data de nascimento, ISO: 2000-05-10T00:00:00
}

export const usuarioService = {
  criar: (d: CriarUsuarioDados) =>
    api.post('/Usuario/Criar', {   // AJUSTA: confere se a rota continua "Criar"
      nome: d.nome,
      sobrenome: d.sobrenome,
      user: d.user,
      senha: d.senha,
      email: d.email,
      cpf: d.cpf,
      dn: d.dn,
    }),
}