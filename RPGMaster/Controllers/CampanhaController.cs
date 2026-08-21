using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RPGMaster.Service;
using RPGMaster.Model.DTOs;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace RPGMaster.Controllers
{
    public class CampanhaController: ControllerBase
    {
        private readonly CampanhaService _campanhaService;

        public CampanhaController (CampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        [Authorize]
        [HttpPost("api/Campanha/Criar")]
        public IActionResult CadastrarCampanha(string nome)
        {
            var result = _campanhaService.CadastrarCampanha(nome);
            if (result)
                return Ok("Campanha criada com sucesso");
            else
                return BadRequest("Erro ao criar a campanha");
        }

        [HttpGet("api/Campanha/ObterTodos")]
        public IActionResult ObterCampanhas()
        {
            var listaCampanhas = _campanhaService.ObterTodos();
            if (listaCampanhas == null)
                throw new Exception("Nenhuma campanha foi encontrada");

            return Ok(listaCampanhas);
        }

        [HttpGet("api/Campanha/ObterTodosPorId")]
        public IActionResult ObterCampanhasPorId(long id)
        {
            var listaCampanhas = _campanhaService.ObterPorId(id);
            if (listaCampanhas == null)
                throw new Exception("Nenhuma campanha foi encontrada");

            return Ok(listaCampanhas);
        }
    }
}
