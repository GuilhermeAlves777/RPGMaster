using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class PericiaService
    {
        private readonly PericiaRepository _periciaRepository;

        public PericiaService(PericiaRepository periciaRepository) => _periciaRepository = periciaRepository;

        public List<PericiaDto> ObterPorCampanha(long idCampanha) =>
            _periciaRepository.ObterPorCampanha(idCampanha);

        public bool Cadastrar(string nome, int valorPadrao, long idCampanha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("A perícia precisa ter um nome");

            var pericia = new Pericia { Nome = nome, Valor_Padrao = valorPadrao, Id_Campanha = idCampanha };
            return _periciaRepository.Cadastrar(pericia);
        }

        public bool Atualizar(long id, long idCampanha, string? nome, int? valorPadrao)
        {
            var pericia = _periciaRepository.ObterEntidadePorId(id, idCampanha)
                ?? throw new Exception("Essa perícia não existe");

            if (nome != null) pericia.Nome = nome;
            if (valorPadrao.HasValue) pericia.Valor_Padrao = valorPadrao.Value;

            return _periciaRepository.Atualizar();
        }

        public bool Deletar(long id, long idCampanha)
        {
            var pericia = _periciaRepository.ObterEntidadePorId(id, idCampanha)
                ?? throw new Exception("Essa perícia não existe");

            return _periciaRepository.Deletar(pericia);
        }
    }
}
