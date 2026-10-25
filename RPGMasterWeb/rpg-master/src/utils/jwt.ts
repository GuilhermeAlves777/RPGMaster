import type { Usuario } from '../types'

const CLAIM_NOME = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
const CLAIM_ID = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'

const texto = (v: unknown) => (typeof v === 'string' && v ? v : undefined)

function lerPayload(token: string): Record<string, unknown> {
  try {
    const base64 = token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/')
    const json = decodeURIComponent(
      atob(base64)
        .split('')
        .map((c) => '%' + c.charCodeAt(0).toString(16).padStart(2, '0'))
        .join('')
    )
    return JSON.parse(json)
  } catch {
    return {}
  }
}

export function usuarioDoToken(token: string): Usuario {
  const p = lerPayload(token)
  return {
    id: Number(p.nameid ?? p[CLAIM_ID] ?? p.sub),
    nome:
      texto(p.unique_name) ?? texto(p.name) ?? texto(p[CLAIM_NOME]) ?? texto(p.sub) ?? 'Aventureiro',
    fotoUrl: texto(p.picture) ?? texto(p.foto),
  }
}