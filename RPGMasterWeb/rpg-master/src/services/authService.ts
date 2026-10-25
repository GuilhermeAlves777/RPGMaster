import { api } from './api'
import type { LoginResponse } from '../types'

export const authService = {
  login: (user: string, senha: string) =>
    api.post<LoginResponse>('/Usuario/Login', { user, senha }), // ajuste rota e campos
}