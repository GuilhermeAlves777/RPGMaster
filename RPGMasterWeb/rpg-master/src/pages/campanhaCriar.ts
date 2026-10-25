import { criacaoService } from '../services/criacaoService'
import type { CampoCriar, Valores } from '../components/ModalCriar'

export type TipoCriar = 'raca' | 'classe' | 'atributo' | 'pericia' | 'item' | 'magia'

interface Config {
  titulo: string
  campos: CampoCriar[]
  salvar: (v: Valores) => Promise<unknown>
}

const NOME: CampoCriar = { chave: 'nome', rotulo: 'Nome', tipo: 'texto', obrigatorio: true, max: 100 }
const DESCRICAO: CampoCriar = { chave: 'descricao', rotulo: 'Descrição', tipo: 'area', max: 500 }
const VALOR_PADRAO: CampoCriar = { chave: 'valorPadrao', rotulo: 'Valor padrão', tipo: 'numero', padrao: 0 }

// AJUSTA: copie os valores reais do seu TiposEnum. O exemplo do Swagger mostra "tipo": 1,
// o que sugere que o enum começa em 1, mas confere no código.
const TIPOS_ITEM = [
  { valor: 1, rotulo: 'Arma' },
  { valor: 2, rotulo: 'Armadura' },
  { valor: 3, rotulo: 'Joia' },
  { valor: 4, rotulo: 'Consumível' },
]

export function configCriar(tipo: TipoCriar, idCampanha: number): Config {
  const s = (v: Valores, k: string) => String(v[k] ?? '').trim()
  const n = (v: Valores, k: string) => Number(v[k] ?? 0)

  switch (tipo) {
    case 'raca':
      return { titulo: 'Nova raça', campos: [NOME, DESCRICAO],
        salvar: v => criacaoService.raca(idCampanha, s(v, 'nome'), s(v, 'descricao')) }

    case 'classe':
      return { titulo: 'Nova classe', campos: [NOME, DESCRICAO],
        salvar: v => criacaoService.classe(idCampanha, s(v, 'nome'), s(v, 'descricao')) }

    case 'atributo':
      return { titulo: 'Novo atributo', campos: [NOME, VALOR_PADRAO],
        salvar: v => criacaoService.atributo(idCampanha, s(v, 'nome'), n(v, 'valorPadrao')) }

    case 'pericia':
      return { titulo: 'Nova perícia', campos: [NOME, VALOR_PADRAO],
        salvar: v => criacaoService.pericia(idCampanha, s(v, 'nome'), n(v, 'valorPadrao')) }

    case 'magia':
      return {
        titulo: 'Nova magia',
        campos: [NOME, { chave: 'dados', rotulo: 'Dados', tipo: 'texto', max: 30, placeholder: 'Ex.: 2d6+3' }],
        salvar: v => criacaoService.magia(idCampanha, s(v, 'nome'), s(v, 'dados')),
      }

    case 'item':
      return {
        titulo: 'Novo item',
        campos: [
          NOME,
          { chave: 'tipo', rotulo: 'Tipo', tipo: 'opcoes', opcoes: TIPOS_ITEM },
          DESCRICAO,
          { chave: 'imagem', rotulo: 'Imagem (URL)', tipo: 'texto', placeholder: 'https://...' },
        ],
        salvar: v => criacaoService.item(idCampanha, s(v, 'nome'), n(v, 'tipo'), s(v, 'descricao'), s(v, 'imagem')),
      }
  }
}