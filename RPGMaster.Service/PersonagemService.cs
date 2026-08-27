using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class PersonagemService
    {
        private readonly PersonagemRepository _personagemRepository;

        public PersonagemService (PersonagemRepository personagemRepository)
        {
            _personagemRepository = personagemRepository;
        }

        public List<Personagem> ObterTodos()
        {
            var listaPersonagens = _personagemRepository.ObterTodos();
            return listaPersonagens;
        }

        public bool CriarPersonagem (string nome, int nivel, long idJogador, int vidaMaxima, int ManaMaxima, long idCampanha)
        {
            if (nome == "") throw new Exception("O personagem precisa ter um nome");
            if (nivel <= 0) throw new Exception("O nível do personagem tem que ser maior que 0");

            var personagem = new Personagem
            {
                Nome = nome,
                Id_Jogador = idJogador != 0 ? idJogador : 0,
                Id_Campanha = idCampanha,
                VidaMaxima = vidaMaxima,
                VidaAtual = vidaMaxima,
                ManaMaxima = ManaMaxima,
                ManaAtual = ManaMaxima,
                EhNpc = idJogador == 0 ? true : false
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
