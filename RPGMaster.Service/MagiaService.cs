using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class MagiaService
    {
        private readonly MagiaRepository _magiaRepository;

        public MagiaService(MagiaRepository magiaRepository) => _magiaRepository = magiaRepository;

        public List<MagiaDto> ObterPorCampanha(long idCampanha) =>
            _magiaRepository.ObterPorCampanha(idCampanha);

        public bool Cadastrar(string nome, string dados, long idCampanha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("A magia precisa ter um nome");

            var magia = new Magia { Nome = nome, Dados = dados, Id_Campanha = idCampanha };
            return _magiaRepository.Cadastrar(magia);
        }

        public bool Atualizar(long id, string? nome, string? dados)
        {
            var magia = _magiaRepository.ObterEntidadePorId(id)
                ?? throw new Exception("Essa magia não existe");

            if (nome != null) magia.Nome = nome;
            if (dados != null) magia.Dados = dados;

            return _magiaRepository.Atualizar();
        }

        public bool Deletar(long id)
        {
            var magia = _magiaRepository.ObterEntidadePorId(id)
                ?? throw new Exception("Essa magia não existe");

            return _magiaRepository.Deletar(magia);
        }
    }
}
