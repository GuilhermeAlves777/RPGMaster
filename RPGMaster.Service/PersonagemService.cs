using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class PersonagemService
    {
        private readonly PersonagemRepository _personagemRepository;
        private readonly CampanhaJogadorRepository _campanhaJogadorRepository;

        public PersonagemService (PersonagemRepository personagemRepository, CampanhaJogadorRepository campanhaJogadorRepository)
        {
            _personagemRepository = personagemRepository;
            _campanhaJogadorRepository = campanhaJogadorRepository;
        }

        public List<Personagem> ObterTodos(long idCampanha)
        {
            var listaPersonagens = _personagemRepository.ObterTodos(idCampanha);
            return listaPersonagens;
        }

        public PersonagemDetalheDto? ObterPorId(long idPersonagem, long idCampanha) =>
            _personagemRepository.ObterPorIdFront(idPersonagem, idCampanha);

        public bool CriarPersonagem (string nome, int nivel, long idJogador, int vidaMaxima, int ManaMaxima, long idCampanha, long idRaca, long idClasse, bool ehNPC)
        {
            if (nome == "") throw new Exception("O personagem precisa ter um nome");
            if (nivel <= 0) throw new Exception("O nível do personagem tem que ser maior que 0");

            var existeCampanhaJogador = _campanhaJogadorRepository.ObterJogador(idCampanha, idJogador);

            if (existeCampanhaJogador == null 
                && !ehNPC) throw new Exception("Esse jogador não está na campanha"); 

            var personagem = new Personagem
            {
                Nome = nome,
                Id_Jogador = idJogador != 0 ? idJogador : null,
                Id_Campanha = idCampanha,
                VidaMaxima = vidaMaxima,
                VidaAtual = vidaMaxima,
                ManaMaxima = ManaMaxima,
                ManaAtual = ManaMaxima,
                Id_Raca = idRaca,
                Id_Classe = idClasse,
                Nivel = nivel,
                EhNpc = ehNPC
            };

            return _personagemRepository.CriarPersonagem(personagem);
        }

        public bool EditarPersonagem(long idPersonagem, long idCampanha, string? nome, int? nivel, int? vidaMaxima, int? manaMaxima)
        {
            var personagem = _personagemRepository.ObterPorId(idPersonagem, idCampanha);

            if (personagem == null)
                throw new Exception("Esse personagem não existe");

            if (nome != null)
                personagem.Nome = nome;

            if (nivel.HasValue)
            {
                if (nivel.Value <= 0)
                    throw new Exception("O nível tem que ser maior que 0");
                personagem.Nivel = nivel.Value;
            }

            if (vidaMaxima.HasValue)
            {
                if (vidaMaxima.Value <= 0)
                    throw new Exception("A vida máxima tem que ser maior que 0");
                personagem.VidaMaxima = vidaMaxima.Value;
            }

            if (manaMaxima.HasValue)
            {
                if (manaMaxima.Value <= 0)
                    throw new Exception("A mana máxima tem que ser maior que 0");
                personagem.ManaMaxima = manaMaxima.Value;
            }

            return _personagemRepository.EditarPersonagem();
        }

        public bool AlterarVida (long idCampanha, long idPersonagem, int? vidaAtual)
        {
            var personagem = _personagemRepository.ObterPorId(idPersonagem, idCampanha);
            if (personagem == null)
                throw new Exception("Esse personagem não existe");

            if (!vidaAtual.HasValue)
                throw new Exception("Necessita colocar um valor");

            if (vidaAtual > personagem.VidaMaxima)
                throw new Exception("O numero inserido sobresai a vida máxima definida para o personagem");

            personagem.VidaAtual = vidaAtual.Value;

            return _personagemRepository.EditarPersonagem();
        }

        public bool AlterarMana(long idCampanha, long idPersonagem, int? manaAtual)
        {
            var personagem = _personagemRepository.ObterPorId(idPersonagem, idCampanha);
            if (personagem == null)
                throw new Exception("Esse personagem não existe");

            if (!manaAtual.HasValue)
                throw new Exception("Necessita colocar um valor");

            if (manaAtual > personagem.ManaMaxima)
                throw new Exception("O numero inserido sobresai a mana máxima definida para o personagem");

            personagem.ManaAtual = manaAtual.Value;

            return _personagemRepository.EditarPersonagem();
        }

        public bool ExcluirPersonagem (long idPersonagem, long idCampanha)
        {
            var personagem = _personagemRepository.ObterPorId(idPersonagem, idCampanha);

            if (personagem == null)
                throw new Exception("Esse personagem não existe");

            var ret = _personagemRepository.ExcluirPersonagem(idPersonagem, idCampanha);

            return ret;
        }
    }
}
