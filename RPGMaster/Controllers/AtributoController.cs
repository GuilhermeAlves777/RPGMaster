using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtributoController : ControllerBase
    {
        private readonly AtributoService _atributoService;

        public AtributoController(AtributoService atributoService) => _atributoService = atributoService;

        [HttpGet("campanha/{idCampanha}")]
        public IActionResult ObterPorCampanha(long idCampanha) =>
            Ok(_atributoService.ObterPorCampanha(idCampanha));

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CadastrarAtributoRequest request)
        {
            var ret = _atributoService.Cadastrar(request.Nome, request.ValorPadrao, request.IdCampanha);
            return ret ? Ok("Atributo cadastrado!") : BadRequest("Não foi possível cadastrar o atributo");
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(long id, [FromBody] AtualizarAtributoRequest request)
        {
            var ret = _atributoService.Atualizar(id, request.Nome, request.ValorPadrao);
            return ret ? Ok("Atributo atualizado!") : BadRequest("Não foi possível atualizar o atributo");
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(long id)
        {
            var ret = _atributoService.Deletar(id);
            return ret ? Ok("Atributo removido!") : BadRequest("Não foi possível remover o atributo");
        }
    }

    public class CadastrarAtributoRequest
    {
        [Required] public string? Nome { get; set; }
        public int ValorPadrao { get; set; }
        [Required] public long IdCampanha { get; set; }
    }

    public class AtualizarAtributoRequest
    {
        public string? Nome { get; set; }
        public int? ValorPadrao { get; set; }
    }
}
