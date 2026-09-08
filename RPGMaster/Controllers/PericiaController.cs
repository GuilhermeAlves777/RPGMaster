using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PericiaController : ControllerBase
    {
        private readonly PericiaService _periciaService;

        public PericiaController(PericiaService periciaService) => _periciaService = periciaService;

        [HttpGet("campanha/{idCampanha}")]
        public IActionResult ObterPorCampanha(long idCampanha) =>
            Ok(_periciaService.ObterPorCampanha(idCampanha));

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CadastrarPericiaRequest request)
        {
            var ret = _periciaService.Cadastrar(request.Nome, request.ValorPadrao, request.IdCampanha);
            return ret ? Ok("Perícia cadastrada!") : BadRequest("Não foi possível cadastrar a perícia");
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(long id, [FromBody] AtualizarPericiaRequest request)
        {
            var ret = _periciaService.Atualizar(id, request.IdCampanha, request.Nome, request.ValorPadrao);
            return ret ? Ok("Perícia atualizada!") : BadRequest("Não foi possível atualizar a perícia");
        }

        [HttpDelete("{id}/{idCampanha}")]
        public IActionResult Deletar(long id, long idCampanha)
        {
            var ret = _periciaService.Deletar(id, idCampanha);
            return ret ? Ok("Perícia removida!") : BadRequest("Não foi possível remover a perícia");
        }
    }

    public class CadastrarPericiaRequest
    {
        [Required] public string Nome { get; set; }
        public int ValorPadrao { get; set; }
        [Required] public long IdCampanha { get; set; }
    }

    public class AtualizarPericiaRequest
    {
        [Required] public long IdCampanha { get; set; }
        public string? Nome { get; set; }
        public int? ValorPadrao { get; set; }
    }
}
