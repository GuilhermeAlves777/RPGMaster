using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class CampanhaJogadorService
    {
        private readonly CampanhaJogadorRepository _campanhaJogadorRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public CampanhaJogadorService(CampanhaJogadorRepository campanhaJogadorRepository, UsuarioRepository usuarioRepository)
        {
            _campanhaJogadorRepository = campanhaJogadorRepository;
            _usuarioRepository = usuarioRepository;
        }

        public bool AdicionarJogador(long idCampanha, string user)
        {
            var jog = _usuarioRepository.ObterPorUser(user);
            var idUsuario = jog == null ? 0 : jog.Id_Usuario;

            if (idUsuario == 0)
                throw new Exception("O usuário não existe!");

            if (_campanhaJogadorRepository.JaParticipa(idCampanha, idUsuario))
                throw new InvalidOperationException("Usuário já participa dessa campanha.");

            var jogador = new Campanha_Jogador
            {
                Id_Campanha = idCampanha,
                Id_Usuario = idUsuario
            };

            return _campanhaJogadorRepository.Adicionar(jogador);
        }
    }
}
