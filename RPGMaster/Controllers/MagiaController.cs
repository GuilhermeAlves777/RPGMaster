using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MagiaController : ControllerBase
    {
        private readonly MagiaService _magiaService;

        public MagiaController(MagiaService magiaService) => _magiaService = magiaService;

        [HttpGet("campanha/{idCampanha}")]
        public IActionResult ObterPorCampanha(long idCampanha) =>
            Ok(_magiaService.ObterPorCampanha(idCampanha));

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CadastrarMagiaRequest request)
        {
            var ret = _magiaService.Cadastrar(request.Nome, request.Dados, request.IdCampanha);
            return ret ? Ok("Magia cadastrada!") : BadRequest("Não foi possível cadastrar a magia");
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(long id, [FromBody] AtualizarMagiaRequest request)
        {
            var ret = _magiaService.Atualizar(id, request.Nome, request.Dados);
            return ret ? Ok("Magia atualizada!") : BadRequest("Não foi possível atualizar a magia");
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(long id)
        {
            var ret = _magiaService.Deletar(id);
            return ret ? Ok("Magia removida!") : BadRequest("Não foi possível remover a magia");
        }
    }

    public class CadastrarMagiaRequest
    {
        [Required] public string? Nome { get; set; }
        [Required] public string? Dados { get; set; }
        [Required] public long IdCampanha { get; set; }
    }

    public class AtualizarMagiaRequest
    {
        public string? Nome { get; set; }
        public string? Dados { get; set; }
    }
}
