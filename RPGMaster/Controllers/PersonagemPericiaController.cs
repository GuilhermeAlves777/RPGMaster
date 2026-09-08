using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagemPericiaController : ControllerBase
    {
        private readonly PersonagemPericiaService _personagemPericiaService;

        public PersonagemPericiaController(PersonagemPericiaService personagemPericiaService) =>
            _personagemPericiaService = personagemPericiaService;

        [HttpGet("personagem/{idPersonagem}")]
        public IActionResult ObterTodos(long idPersonagem) =>
            Ok(_personagemPericiaService.ObterTodos(idPersonagem));

        [HttpPost]
        public IActionResult Adicionar([FromBody] AdicionarPersonagemPericiaRequest request)
        {
            var ret = _personagemPericiaService.Adicionar(request.IdPersonagem, request.IdPericia, request.IdCampanha, request.Valor);
            return ret ? Ok("Perícia adicionada ao personagem!") : BadRequest("Não foi possível adicionar a perícia");
        }

        [HttpPut]
        public IActionResult Editar([FromBody] EditarPersonagemPericiaRequest request)
        {
            var ret = _personagemPericiaService.Editar(request.IdPersonagem, request.IdCampanha, request.IdPericia, request.Valor);
            return ret ? Ok("Perícia atualizada!") : BadRequest("Não foi possível atualizar a perícia");
        }

        [HttpDelete("{idPersonagem}/{idPericia}")]
        public IActionResult Excluir(long idPersonagem, long idPericia)
        {
            var ret = _personagemPericiaService.Excluir(idPersonagem, idPericia);
            return ret ? Ok("Perícia removida do personagem!") : BadRequest("Não foi possível remover a perícia");
        }
    }

    public class AdicionarPersonagemPericiaRequest
    {
        [Required] public long IdPersonagem { get; set; }
        [Required] public long IdPericia { get; set; }
        [Required] public long IdCampanha { get; set; }
        public int Valor { get; set; }
    }

    public class EditarPersonagemPericiaRequest
    {
        [Required] public long IdPersonagem { get; set; }
        [Required] public long IdCampanha { get; set; }
        [Required] public long IdPericia { get; set; }
        public int Valor { get; set; }
    }
}
