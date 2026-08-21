using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
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

        public List<CampanhaDto> ObterPorId(long id)
        {
            var listaCampanhas = _campanhaRepository.ObterPorId(id);
            return listaCampanhas;
        }
    }
}
