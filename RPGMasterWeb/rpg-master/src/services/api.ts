const BASE_URL = '/api'

async function request<T>(path: string, options: RequestInit = {}): Promise<T> {
  const token = localStorage.getItem('token')

  const res = await fetch(`${BASE_URL}${path}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...(token && { Authorization: `Bearer ${token}` }),
    },
  })

  if (res.status === 401) {
  if (token) {
    // havia sessão e ela expirou
    localStorage.removeItem('token')
    window.location.href = '/login'
    throw new Error('Sessão expirada')
  }
  // sem token: é tentativa de login com credenciais erradas
  throw new Error('Usuário ou senha inválidos')
}
    const texto = await res.text()

  if (!res.ok) {
    // mostra a mensagem que o back mandou (ex.: regra de negócio), se houver
    throw new Error(texto || `Erro ${res.status}`)
  }

  if (!texto) return undefined as T

  const ehJson = res.headers.get('content-type')?.includes('application/json')
  return (ehJson ? JSON.parse(texto) : texto) as T

}

export const api = {
  get: <T>(path: string) => request<T>(path),
  post: <T>(path: string, body: unknown) =>
    request<T>(path, { method: 'POST', body: JSON.stringify(body) }),
  put: <T>(path: string, body: unknown) =>
    request<T>(path, { method: 'PUT', body: JSON.stringify(body) }),
  delete: <T = void>(path: string) => request<T>(path, { method: 'DELETE' }),
}