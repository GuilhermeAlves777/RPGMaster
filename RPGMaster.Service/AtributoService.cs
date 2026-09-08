using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class AtributoService
    {
        private readonly AtributoRepository _atributoRepository;

        public AtributoService(AtributoRepository atributoRepository) => _atributoRepository = atributoRepository;

        public List<AtributoDto> ObterPorCampanha(long idCampanha) =>
            _atributoRepository.ObterPorCampanha(idCampanha);

        public bool Cadastrar(string nome, int valorPadrao, long idCampanha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O atributo precisa ter um nome");

            var atributo = new Atributo { Nome = nome, Valor_Padrao = valorPadrao, Id_Campanha = idCampanha };
            return _atributoRepository.Cadastrar(atributo);
        }

        public bool Atualizar(long id, long idCampanha, string? nome, int? valorPadrao)
        {
            var atributo = _atributoRepository.ObterEntidadePorId(id, idCampanha)
                ?? throw new Exception("Esse atributo não existe");

            if (nome != null) atributo.Nome = nome;
            if (valorPadrao.HasValue) atributo.Valor_Padrao = valorPadrao.Value;

            return _atributoRepository.Atualizar();
        }

        public bool Deletar(long id)
        {
            var atributo = _atributoRepository.ObterEntidadePorId(id)
                ?? throw new Exception("Esse atributo não existe");

            return _atributoRepository.Deletar(atributo);
        }
    }
}
