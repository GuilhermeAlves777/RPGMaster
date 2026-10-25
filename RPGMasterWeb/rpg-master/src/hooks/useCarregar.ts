import { useEffect, useState } from 'react'

export interface Carregamento<T> {
  dados: T | null
  loading: boolean
  erro: string | null
  recarregar: () => void
}

export function useCarregar<T>(buscar: () => Promise<T>, deps: unknown[]): Carregamento<T> {
  const [dados, setDados] = useState<T | null>(null)
  const [loading, setLoading] = useState(true)
  const [erro, setErro] = useState<string | null>(null)
  const [versao, setVersao] = useState(0)

  useEffect(() => {
    let ativo = true
    setLoading(true)
    setErro(null)
    buscar()
      .then((d) => ativo && setDados(d))
      .catch((e) => ativo && setErro(e instanceof Error ? e.message : 'Erro ao carregar'))
      .finally(() => ativo && setLoading(false))
    return () => {
      ativo = false
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [...deps, versao])

  return { dados, loading, erro, recarregar: () => setVersao((v) => v + 1) }
}