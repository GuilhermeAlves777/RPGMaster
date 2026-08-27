using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RPGMaster.Service;
using RPGMaster.Model.DTOs;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace RPGMaster.Controllers
{
    public class PersonagemController: ControllerBase
    {
        private readonly PersonagemService _personagemService;

        public PersonagemController (PersonagemService personagemService)
        {
           _personagemService = personagemService;
        }

        [HttpGet("api/Personagem/ObterTodos")]

        public IActionResult ObterTodos()
        {
            var result = _personagemService.ObterTodos();
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Não foi possivel buscar os personagem disponiveis");
        }

        [HttpPost("api/Personagem/Criar")]

        public IActionResult CriarPersonagem (string nome, int nivel, long idJogador, int vidaMaxima, int ManaMaxima, long idCampanha)
        {
            var result = _personagemService.CriarPersonagem(nome, nivel, idJogador, vidaMaxima, ManaMaxima, idCampanha);

            if (result)
                return Ok("Personagem Criado");
            else
                return BadRequest("Personagem não pode ser criado");
        }

        [HttpPut("api/Personagem/EditarPersonagem")]

        public IActionResult EditarPersonagem(long idPersonagem, long idCampanha, string? nome, int? nivel, int? vidaMaxima, int? manaMaxima)
        {
            var result = _personagemService.EditarPersonagem(idPersonagem, idCampanha, nome, nivel, vidaMaxima, manaMaxima);

            if (result)
                return Ok("O personagem " + nome + " foi editado com sucesso");
            else
                return BadRequest("Não foi possivel editar o personagem");
        }

        [HttpDelete("api/Personagem/Excluir")]

        public IActionResult ExcluirPersonagem (long idPersonagem, long idCampanha)
        {
            var result = _personagemService.ExcluirPersonagem(idPersonagem, idCampanha);

            if (result)
                return Ok("O personagem foi excluido com sucesso");
            else
                return BadRequest("Não foi possivel editar o personagem");
        }
    }
}
