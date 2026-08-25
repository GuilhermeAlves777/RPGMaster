using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RPGMaster.Model.DTOs;

namespace RPGMaster.Service
{
    public class CampanhaJogadorService
    {
        private readonly CampanhaJogadorRepository _campanhaJogadorRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly CampanhaRepository _campanhaRepository;

        public CampanhaJogadorService(CampanhaJogadorRepository campanhaJogadorRepository, UsuarioRepository usuarioRepository, CampanhaRepository campanhaRepository)
        {
            _campanhaJogadorRepository = campanhaJogadorRepository;
            _usuarioRepository = usuarioRepository;
            _campanhaRepository = campanhaRepository;
        }

        public List<Campanha_Jogador> ObterTodos ()
        {
            var listaJogadores = _campanhaJogadorRepository.ObterTodos();
            return listaJogadores;
        }

        public List<CampanhaJogadorDto> ObterJogadorPorCampanha(long idCampanha)
        {
            var jogadores = _campanhaJogadorRepository.ObterJogadorPorCampanha(idCampanha);

            if (jogadores == null)
                throw new Exception("Não há nenhum jogador nessa campanha");

            return jogadores;
        }

        public bool AdicionarJogador(long idCampanha, string user)
        {
            var jog = _usuarioRepository.ObterPorUser(user);
            var idUsuario = jog == null ? 0 : jog.Id_Usuario;
            var campanha = _campanhaRepository.ObterPorIdSemDto(idCampanha);

            if (jog == null)
                throw new Exception("O usuário não existe!");

            if (campanha == null)
                throw new Exception("A campanha não existe");

            if (idUsuario == campanha.ID_Criador)
                throw new Exception("O usuário é o mestre da campanha");

            if (_campanhaJogadorRepository.JaParticipa(idCampanha, idUsuario))
                throw new InvalidOperationException("Usuário já participa dessa campanha.");

            var jogador = new Campanha_Jogador
            {
                Id_Campanha = idCampanha,
                Id_Usuario = idUsuario
            };

            return _campanhaJogadorRepository.Adicionar(jogador);
        }

        public bool ExcluirJogador(long idCampanha, long idUsuario) {
            var jogador = _campanhaJogadorRepository.ObterJogador(idCampanha, idUsuario);,
            var campanha = _campanhaRepository.ObterPorId(idCampanha);

            if (jogador == null)
                throw new Exception("Esse jogador não está na campanha");

            if (campanha == null)
                return false;

            if (idUsuario == campanha.Id_Criador)
                throw new Exception("Você não pode apagar o mestre da campanha!");
            
            var ret = _campanhaJogadorRepository.ExcluirJogador(jogador); 
            
            return ret;       
        }
    }
}
