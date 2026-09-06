using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class ClasseService
    {
        private readonly ClasseRepository _classeRepository;
        private readonly CampanhaRepository _campanhaRepository;

        public ClasseService (ClasseRepository classeRepository, CampanhaRepository campanhaRepository)
        {
            _classeRepository = classeRepository;
            _campanhaRepository = campanhaRepository;
        }

        public List<Classe> ObterTodos (long idCampanha)
        {
            return _classeRepository.ObterTodos(idCampanha);
        }

        public Classe? ObterPorId (long id, long idCampanha)
        {
            return _classeRepository.ObterPorId(id, idCampanha);
        }

        public bool AdicionarClasse (long idCampanha, string nome, string descricao)
        {
            var campanha = _campanhaRepository.ObterPorId(idCampanha);

            if (campanha == null) throw new Exception("A campanha não existe");

            var classe = new Classe {
                Id_Campanha = idCampanha,
                Nome = nome,
                Descricao = descricao
            };

            return _classeRepository.AdicionarClasse(classe);
        }

        public bool EditarClasse (long id, long idCampanha, string nome, string descricao)
        {
            var classe = _classeRepository.ObterPorId(id, idCampanha);

            if (classe == null) throw new Exception("A classe não existe");

            if (nome != null)
            {
                if (_classeRepository.ObterTodos(idCampanha).Any(r => r.Nome == nome))
                    throw new Exception("Uma raça já possui esse nome nessa campanha");
                else
                    classe.Nome = nome;
            }

            if (descricao != null) classe.Descricao = descricao;

            return _classeRepository.EditarClasse();
        }

        public bool ExcluirClasse (long id, long idCampanha)
        {
            var classe = _classeRepository.ObterPorId(id, idCampanha);

            if (classe == null) throw new Exception("A classe não existe");

            return _classeRepository.ExcluirClasse(id, idCampanha);
        }
    }
}
