import Bloco from './Bloco'
import type { Carregamento } from '../hooks/useCarregar'
import type { CatalogoDto } from '../types'
import styles from './Bloco.module.css'

interface Props {
  titulo: string
  estado: Carregamento<CatalogoDto[]>
  podeAdicionar: boolean
  onAdicionar?: () => void
}

export default function BlocoCatalogo({ titulo, estado, podeAdicionar, onAdicionar }: Props) {
  return (
    <Bloco
      titulo={titulo}
      podeAdicionar={podeAdicionar}
      onAdicionar={onAdicionar}
      loading={estado.loading}
      erro={estado.erro}
      vazio={estado.dados?.length === 0}
    >
      <ul className={styles.lista}>
        {estado.dados?.map((item) => (
          <li key={item.id} className={styles.item}>
            {item.nome}
          </li>
        ))}
      </ul>
    </Bloco>
  )
}