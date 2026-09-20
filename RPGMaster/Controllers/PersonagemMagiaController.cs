using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagemMagiaController : ControllerBase
    {
        private readonly PersonagemMagiaService _personagemMagiaService;

        public PersonagemMagiaController (PersonagemMagiaService personagemMagiaService)
        {
            _personagemMagiaService = personagemMagiaService;
        }

        [HttpGet("ObterTodos")]

        public IActionResult ObterTodos (long idPersonagem)
        {
            var ret = _personagemMagiaService.ObterTodos (idPersonagem);

            if (ret == null) BadRequest("Não foi possivel buscar as magias do personagem");

            return Ok(ret);
        }

        [HttpPost("Adicionar")]

        public IActionResult Adicionar(long idCampanha, long idPersonagem, long idMagia)
        { 
            var ret = _personagemMagiaService.Adicionar(idPersonagem, idMagia, idCampanha);

            if (!ret) BadRequest("Não foi possível atrelar a magia a esse personagem");

            return Ok("Magia foi atrelada ao personagem");
        }

        [HttpDelete("Excluir")]

        public IActionResult Excluir (long idPersonsagem, long idMagia)
        {
            var ret = _personagemMagiaService.Excluir(idPersonsagem, idMagia);

            if (!ret) BadRequest("Nao foi possivel retirar a magia desse personagem!");

            return Ok("Magia removida do´personagem");
        }
    }
}
