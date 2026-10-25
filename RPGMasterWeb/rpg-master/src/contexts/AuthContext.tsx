import { createContext, useContext, useMemo, useState } from 'react'
import type { ReactNode } from 'react'
import { authService } from '../services/authService'
import { usuarioDoToken } from '../utils/jwt'
import type { Usuario } from '../types'

interface AuthContextData {
  token: string | null
  usuario: Usuario | null
  isAuth: boolean
  login: (usuario: string, senha: string) => Promise<void>
  logout: () => void
}

const AuthContext = createContext<AuthContextData | null>(null)

export function AuthProvider({ children }: { children: ReactNode }) {
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'))

  const usuario = useMemo(() => (token ? usuarioDoToken(token) : null), [token])

  async function login(user: string, senha: string) {
    const { token } = await authService.login(user, senha)
    localStorage.setItem('token', token)
    setToken(token)
  }

  function logout() {
    localStorage.removeItem('token')
    setToken(null)
  }

  return (
    <AuthContext.Provider value={{ token, usuario, isAuth: !!token, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth deve ser usado dentro de <AuthProvider>')
  return ctx
}