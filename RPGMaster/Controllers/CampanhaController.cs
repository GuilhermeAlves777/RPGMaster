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
        [HttpPost("Criar")]
        public IActionResult CadastrarCampanha(string nome)
        {
            var result = _campanhaService.CadastrarCampanha(nome);
            if (result)
                return Ok("Campanha criada com sucesso");
            else
                return BadRequest("Erro ao criar a campanha");
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterCampanhas()
        {
            var listaCampanhas = _campanhaService.ObterTodos();
            return Ok(listaCampanhas);
        }
    }
}
