using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace RPGMaster.Service
{
    public class CampanhaService
    {
        private readonly CampanhaRepository _campanhaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CampanhaService (CampanhaRepository campanhaRepository, IHttpContextAccessor httpContextAccessor)
        {
            _campanhaRepository = campanhaRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CadastrarCampanha(string nome)
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (idClaim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            long idCriador = long.Parse(idClaim);

            var cam = new Campanha()
            {
                Nome = nome,
                ID_Criador = idCriador
            };

            var ret = _campanhaRepository.Cadastrar(cam);

            return ret;

        }
        public List<CampanhaDto> ObterTodos()
        {
            var listaCampanhas = _campanhaRepository.ObterTodas();
            return listaCampanhas;
        }

        public CampanhaDto? ObterPorId(long id)
        {
            var campanha = _campanhaRepository.ObterPorId(id);

            if (campanha == null)
                throw new Exception("Essa campanha não existe");

            return campanha;
        }

        public bool AtualizarCampanha (long id, string nome)
        {
            var campanha = _campanhaRepository.ObterPorIdSemDto(id);

            if (campanha == null)
                throw new Exception("Essa campanha não existe");

            campanha.Nome = nome;

            var ret = _campanhaRepository.Atualizar();

            return ret;
        }

        public bool Excluir (long id)
        {
            var campanha = _campanhaRepository.ObterPorIdSemDto(id);

            if (campanha == null)
                throw new Exception("Não é possível excluir a campanha selecionada");

            var ret = _campanhaRepository.Excluir(campanha);

            return ret;
        }
    }
}
