import { Routes, Route, Navigate } from 'react-router-dom'
import PrivateRoute from './routes/PrivateRoute'
import AppLayout from './layouts/AppLayout'
import Login from './pages/Login'
import Campanhas from './pages/Campanhas'
import Personagens from './pages/Personagens'
import { PersonagemDetalhePage } from './pages/PersonagemDetalhe'
import CampanhaDetalhe from './pages/CampanhaDetalhe'
import CriarUsuario from './pages/CriarUsuario'
import CriarCampanha from './pages/CriarCampanha'
import ConvitePagina from './pages/ConvitePagina'
import CriarPersonagem from './pages/CriarPersonagem'

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/cadastro" element={<CriarUsuario />} />
      <Route element={<PrivateRoute />}>
        <Route element={<AppLayout />}>
          <Route path="/campanhas" element={<Campanhas />} />
          <Route path="/campanhas/nova" element={<CriarCampanha />} />
          <Route path="/campanhas/:idCampanha/personagens" element={<Personagens />} />
          <Route path="/personagens/:idPersonagem/:idCampanha" element={<PersonagemDetalhePage />} />
          <Route path="/campanhas/:idCampanha" element={<CampanhaDetalhe />} />
          <Route path="/convite/:codigo" element={<ConvitePagina />} />
          <Route path="/campanhas/:idCampanha/personagens/novo" element={<CriarPersonagem />} />
        </Route>
      </Route>

      <Route path="*" element={<Navigate to="/campanhas" replace />} />
    </Routes>
  )
}