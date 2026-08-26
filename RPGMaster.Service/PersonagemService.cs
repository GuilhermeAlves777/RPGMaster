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

        public bool CriarPersonagem (string nome, int nivel, int idJogador, int vidaMaxima, int ManaMaxima, long idCampanha)
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
    }
}
