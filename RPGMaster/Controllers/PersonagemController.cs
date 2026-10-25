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

        public IActionResult ObterTodos(long idCampanha)
        {
            var result = _personagemService.ObterTodos(idCampanha);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Não foi possivel buscar os personagens disponiveis");
        }

        [HttpGet("api/Personagem/ObterPorId")]

        public IActionResult ObterPorId(long idPersonagem, long idCampanha)
        {
            var result = _personagemService.ObterPorId(idPersonagem, idCampanha);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Não foi possivel buscar o personagem");
        }

        [HttpPost("api/Personagem/Criar")]

        public IActionResult CriarPersonagem ([FromBody] CriarPersonagem request)
        {
            var result = _personagemService.CriarPersonagem(request.nome, request.nivel, request.idJogador, request.vidaMaxima, 
                request.manaMaxima, request.idCampanha, request.idRaca, request.idClasse, request.ehNPC);

            if (result)
                return Ok("Personagem Criado");
            else
                return BadRequest("Personagem não pode ser criado");
        }

        [HttpPut("api/Personagem/EditarPersonagem")]

        public IActionResult EditarPersonagem([FromBody] EditarPersonagemRequest request)
        {
            var result = _personagemService.EditarPersonagem(
                idPersonagem: request.IdPersonagem,
                idCampanha: request.IdCampanha,
                nome: request.Nome,
                nivel: request.Nivel,
                vidaMaxima: request.VidaMaxima,
                manaMaxima: request.ManaMaxima);

            if (result)
                return Ok("O personagem foi editado com sucesso");
            else
                return BadRequest("Não foi possivel editar o personagem");
        }

        [HttpPut("api/Personagem/AlterarVida")]

        public IActionResult AlterarVida([FromBody] AlterarVidaRequest request)
        {
            var result = _personagemService.AlterarVida(request.IdCampanha, request.IdPersonagem, request.VidaAtual);

            if (result)
                return Ok("Os pontos de vida do personagem foi alterada");
            else
                return BadRequest("Não foi possivel alterar a vida do personagem");
        }

        [HttpPut("api/Personagem/AlterarMana")]

        public IActionResult AlterarMana([FromBody] AlterarManaRequest request)
        {
            var result = _personagemService.AlterarMana(request.IdCampanha, request.IdPersonagem, request.manaAtual);

            if (result)
                return Ok("Os pontos de mana do personagem foi alterada");
            else
                return BadRequest("Não foi possivel alterar a mana do personagem");
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

    public class CriarPersonagem
    {
        public string nome { get; set; }
        public int nivel { get; set; }
        public long idJogador { get; set; }
        public int vidaMaxima { get; set; }
        public int manaMaxima { get; set; }
        public long idCampanha { get; set; }
        public long idRaca { get; set; }
        public long idClasse { get; set; }
        public bool ehNPC { get; set; }
    }

    public class AlterarVidaRequest
    {
        public long IdPersonagem { get; set; }
        public long IdCampanha { get; set; }
        public int VidaAtual { get; set; }
    }

    public class AlterarManaRequest
    {
        public long IdPersonagem { get; set; }
        public long IdCampanha { get; set; }
        public int manaAtual { get; set; }
    }

    public class EditarPersonagemRequest
    {
        public long IdPersonagem { get; set; }
        public long IdCampanha { get; set; }
        public string? Nome { get; set; }
        public int? Nivel { get; set; }
        public int? VidaMaxima { get; set; }
        public int? ManaMaxima { get; set; }
    }
}
