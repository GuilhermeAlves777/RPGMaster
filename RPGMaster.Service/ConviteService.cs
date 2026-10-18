using Microsoft.AspNetCore.Http;
using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RPGMaster.Service
{
    public class ConviteService
    {
        private readonly ConviteRepository _repos;
        private readonly CampanhaRepository _campanhaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CampanhaJogadorRepository _campanhaJogadorRepository;


        public ConviteService(ConviteRepository repos, 
            CampanhaRepository campanhaRepository, 
            IHttpContextAccessor httpContextAccessor, 
            CampanhaJogadorRepository campanhaJogadorRepository)
        {
            _repos = repos;
            _campanhaRepository = campanhaRepository;
            _httpContextAccessor = httpContextAccessor;
            _campanhaJogadorRepository = campanhaJogadorRepository;
        }

        public string GerarConvite(long idCampanha)
        {

            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var campanha = _campanhaRepository.ObterPorIdSemDto(idCampanha);

            if (campanha == null) throw new Exception("A campanha não existe");

            if (idClaim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            long idJogador = long.Parse(idClaim);

            if (idJogador != campanha.ID_Criador) throw new Exception("Você não é o criador desta campanha");

            var convitesAtivos = _repos.ObterAtivoPorCampanha(idCampanha);

            foreach (var c in convitesAtivos)
            {
                c.Ativo = false;
            }

            var bytes = RandomNumberGenerator.GetBytes(9);
            var codigo = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');

            var convite = new Convite()
            {
                Id_Campanha = campanha.ID_Campanha,
                Codigo = codigo,
                Ativo = true,
                CriadoEm = DateTime.UtcNow
            };

            var ret = _repos.GerarConvite(convite);

            if (!ret) throw new Exception("Não foi possível criar o convite");

            return codigo;
        }

        public string ObterConvite(long idCampanha)
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var campanha = _campanhaRepository.ObterPorIdSemDto(idCampanha);

            if (campanha == null) throw new Exception("A campanha não existe");

            if (idClaim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            long idJogador = long.Parse(idClaim);

            if (idJogador != campanha.ID_Criador) throw new Exception("Você não é o criador desta campanha");

            var convite =  _repos.ObterUltimoAtivo(idCampanha);

            if (convite == null) throw new Exception("Necessário gerar um convite");

            return convite.Codigo; 
        }

        public ConviteDto? ObterConvite(string codigo) =>
            _repos.ConviteRecebido(codigo);

        public bool AceiteConvite (string codigo)
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (idClaim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            long idJogador = long.Parse(idClaim);

            var convite = _repos.ConviteRecebido(codigo);
            if (convite == null) throw new Exception("Esse convite é inválido");

            var campanha = _campanhaRepository.ObterPorIdSemDto(convite.Id_Campanha);

            if (campanha == null) throw new Exception("Campanha não existe");

            if (_campanhaJogadorRepository.JaParticipa(convite.Id_Campanha, idJogador) ||
                campanha.ID_Criador == idJogador)
                throw new Exception("O jogador já participa dessa campanha");

            var cj = new Campanha_Jogador()
            {
                Id_Campanha = convite.Id_Campanha,
                Id_Usuario = idJogador
            };

            var ret = _campanhaJogadorRepository.Adicionar(cj);

            if (!ret) throw new Exception("Não foi possivel adicionar o jogador a campanha");

            return ret;

        }
    }
}
