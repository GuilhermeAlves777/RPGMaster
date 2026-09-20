using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class PersonagemMagiaService
    {
        private readonly PersonagemMagiaRepository _personagemMagiaRepository;
        private readonly PersonagemRepository _personagemRepository;
        private readonly MagiaRepository _magiaRepository;
        private readonly CampanhaRepository _campanhaRepository;

        public PersonagemMagiaService (PersonagemMagiaRepository personagemMagiaRepository, 
            CampanhaRepository campanhaRepository, 
            PersonagemRepository personagemRepository,
            MagiaRepository magiaRepository)
        {
            _personagemMagiaRepository = personagemMagiaRepository;
            _campanhaRepository = campanhaRepository;
            _magiaRepository = magiaRepository;
            _personagemRepository = personagemRepository;
        }

        public List<Personagem_Magia> ObterTodos (long idPersonagem) => _personagemMagiaRepository.ObterTodos (idPersonagem);

        public bool Adicionar (long idPersonagem, long idMagia, long idCampanha)
        {
            var p = _personagemRepository.ObterPorId(idPersonagem, idCampanha);
            var m = _magiaRepository.ObterEntidadePorId(idMagia);

            if (m == null || p == null) throw new Exception("Magia ou Personagem não existem no sistema!");

            if (_personagemMagiaRepository.JaPossui(idPersonagem, idMagia)) throw new Exception("O personagem já possui essa magia");

            var pm = new Personagem_Magia {
                Id_Magia = idMagia,
                Id_Personagem = idPersonagem,
            };

            var ret = _personagemMagiaRepository.Adicionar(pm);

            if (!ret) throw new Exception("Não foi possível atrlar a magia ao personagem");

            return ret;
        }

        public bool Excluir (long idPersonagem, long idMagia)
        {
            var pm = _personagemMagiaRepository.ObterPorId(idPersonagem, idMagia);

            if (pm == null) throw new Exception("Essa magia não está mais atrelada a esse personagem");

            var ret = _personagemMagiaRepository.Excluir(pm);

            return ret;
        }

    }
}
 