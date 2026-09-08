using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace RPGMaster.Service
{
    public class PersonagemAtributoService
    {
        private readonly PersonagemAtributoRepository _personagemAtributoRepository;
        private readonly PersonagemRepository _personagemRepository;
        private readonly AtributoRepository _atributoRepository;

        public PersonagemAtributoService(PersonagemAtributoRepository personagemAtributoRepository, 
            PersonagemRepository personagemRepository, 
            AtributoRepository atributoRepository)
        {
            _personagemAtributoRepository = personagemAtributoRepository;
            _personagemRepository = personagemRepository;
            _atributoRepository = atributoRepository;
        }

        public List<Personagem_Atributo> ObterTodos (long idPersonagem) =>
            _personagemAtributoRepository.ObterTodos (idPersonagem);

        public bool Adicionar (long idPersonagem, long idAtributo, long idCampanha, int valor)
        {
            var atr = _atributoRepository.ObterEntidadePorId(idAtributo, idCampanha);
            var per = _personagemRepository.ObterPorId(idPersonagem, idCampanha);

            if (atr == null || per == null) throw new Exception("Personagem ou atributo não existem na campanha");
            if (_personagemAtributoRepository.JaPossui(idPersonagem, idAtributo)) throw new Exception("Personagem já possui esse atributo");

            var pa = new Personagem_Atributo
            {
                Id_Atributo = idAtributo,
                Id_Personagem = idPersonagem,
                Valor = valor
            };

            var ret = _personagemAtributoRepository.Adicionar(pa);
            if (!ret) throw new Exception("Não foi possivel adicionar o atributo ao personagem");

            return ret;
        }

        public bool Editar (long idPersonagem, long idCampanha, long idAtributo, int valor)
        {
            var atr = _atributoRepository.ObterEntidadePorId(idAtributo, idCampanha);
            var per = _personagemRepository.ObterPorId(idPersonagem, idCampanha);
            if (atr == null || per == null) throw new Exception("Personagem ou atributo não existem na campanha");

            var pa = _personagemAtributoRepository.Obter(idPersonagem, idAtributo);
            if (pa == null) throw new Exception("Esse atributo não existe para esse personagem");

            pa.Valor = valor;

            var ret = _personagemAtributoRepository.Editar();

            if (!ret) throw new Exception("Não foi possivel editar a atribuição");

            return ret;
        }

        public bool Excluir (long idPersonagem, long idAtributo, long idCampanha)
        {
            var pa = _personagemAtributoRepository.Obter(idPersonagem, idAtributo);

            if (pa == null) throw new Exception("Essa atribuição não existe ao personagem");

            var ret = _personagemAtributoRepository.Excluir(pa);

            return ret;
        }
    }
}
