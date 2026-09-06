using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class RacaService
    {
        private readonly RacaRepository _repository;
        private readonly CampanhaRepository _campanhaRepository;

        public RacaService (RacaRepository repository, CampanhaRepository campanhaRepository)
        {
            _repository = repository;
            _campanhaRepository = campanhaRepository;
        }

        public List<Raca> ObterTodos(long idCampanha)
        {
            return _repository.ObterTodos(idCampanha);
        }

        public bool CriarRaca (long idCampanha, string nome, string descricao)
        {
            var campanha = _campanhaRepository.ObterPorId(idCampanha);
            //var raca = _repository.ObterTodos().ToList();

            if (_repository.ObterTodos(idCampanha).Any(r => r.Nome == nome)) throw new Exception ("Uma raça já possui esse nome nessa campanha");

            if (campanha == null) throw new Exception("A campanha não existe");

            var raca = new Raca
            {
                Id_Campanha = idCampanha,
                Nome = nome,
                Descricao = descricao
            };

            var ret = _repository.CriarRaca(raca);

            return ret;
        }

        public bool EditarRaca (long idRaca, string nome, string descricao, long idCampanha)
        {
            var raca = _repository.ObterPorId(idRaca, idCampanha);

            if (raca == null) throw new Exception("Essa raça não existe na campanha");
            
            if (nome != null)
            {
                if (_repository.ObterTodos(idCampanha).Any(r => r.Nome == nome))
                    throw new Exception("Uma raça já possui esse nome nessa campanha");
                else
                    raca.Nome = nome;
            }
               
            if (descricao != null) raca.Descricao = descricao;

            return _repository.EditarRaca(raca);
        }

        public bool ExcluirRaca (long idRaca, long idCampanha)
        {
            var raca = _repository.ObterPorId(idRaca, idCampanha);

            if (raca == null) throw new Exception("Esta raça não existe");

            var ret = _repository.ExcluirRaca(idRaca, idCampanha);

            return ret;   
        }
    }
}
