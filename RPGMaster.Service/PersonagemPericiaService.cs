using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class PersonagemPericiaService
    {
        private readonly PersonagemPericiaRepository _personagemPericiaRepository;
        private readonly PersonagemRepository _personagemRepository;
        private readonly PericiaRepository _periciaRepository;

        public PersonagemPericiaService(PersonagemPericiaRepository personagemPericiaRepository,
            PersonagemRepository personagemRepository,
            PericiaRepository periciaRepository)
        {
            _personagemPericiaRepository = personagemPericiaRepository;
            _personagemRepository = personagemRepository;
            _periciaRepository = periciaRepository;
        }

        public List<Personagem_Pericia> ObterTodos(long idPersonagem) =>
            _personagemPericiaRepository.ObterTodos(idPersonagem);

        public bool Adicionar(long idPersonagem, long idPericia, long idCampanha, int valor)
        {
            var per = _periciaRepository.ObterEntidadePorId(idPericia, idCampanha);
            var pers = _personagemRepository.ObterPorId(idPersonagem, idCampanha);

            if (per == null || pers == null) throw new Exception("Personagem ou perícia não existem na campanha");
            if (_personagemPericiaRepository.JaPossui(idPersonagem, idPericia)) throw new Exception("Personagem já possui essa perícia");

            var pp = new Personagem_Pericia
            {
                Id_Pericia = idPericia,
                Id_Personagem = idPersonagem,
                Valor = valor
            };

            var ret = _personagemPericiaRepository.Adicionar(pp);
            if (!ret) throw new Exception("Não foi possível adicionar a perícia ao personagem");

            return ret;
        }

        public bool Editar(long idPersonagem, long idCampanha, long idPericia, int valor)
        {
            var per = _periciaRepository.ObterEntidadePorId(idPericia, idCampanha);
            var pers = _personagemRepository.ObterPorId(idPersonagem, idCampanha);
            if (per == null || pers == null) throw new Exception("Personagem ou perícia não existem na campanha");

            var pp = _personagemPericiaRepository.Obter(idPersonagem, idPericia);
            if (pp == null) throw new Exception("Essa perícia não existe para esse personagem");

            pp.Valor = valor;

            var ret = _personagemPericiaRepository.Editar();
            if (!ret) throw new Exception("Não foi possível editar a atribuição");

            return ret;
        }

        public bool Excluir(long idPersonagem, long idPericia)
        {
            var pp = _personagemPericiaRepository.Obter(idPersonagem, idPericia);
            if (pp == null) throw new Exception("Essa atribuição não existe ao personagem");

            return _personagemPericiaRepository.Excluir(pp);
        }
    }
}
