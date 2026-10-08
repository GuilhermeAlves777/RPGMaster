using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Model.Enums;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArmaDetalheController : ControllerBase
    {
        private readonly ArmaDetalheService _armaDetalheService;

        public ArmaDetalheController(ArmaDetalheService armaDetalheService) => _armaDetalheService = armaDetalheService;

        [HttpGet("item/{idItem}")]
        public IActionResult ObterPorItem(long idItem) =>
            Ok(_armaDetalheService.ObterPorItem(idItem));

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CadastrarArmaDetalheRequest request)
        {
            var ret = _armaDetalheService.Cadastrar(request.IdItem, request.IdCampanha, request.Dano, request.TipoDano, request.Alcance);
            return ret ? Ok("Detalhes de arma cadastrados!") : BadRequest("Não foi possível cadastrar");
        }

        [HttpPut("{idItem}")]
        public IActionResult Atualizar(long idItem, [FromBody] AtualizarArmaDetalheRequest request)
        {
            var ret = _armaDetalheService.Atualizar(idItem, request.Dano, request.TipoDano, request.Alcance);
            return ret ? Ok("Detalhes de arma atualizados!") : BadRequest("Não foi possível atualizar");
        }

        [HttpDelete("{idItem}")]
        public IActionResult Deletar(long idItem)
        {
            var ret = _armaDetalheService.Deletar(idItem);
            return ret ? Ok("Detalhes de arma removidos!") : BadRequest("Não foi possível remover");
        }
    }

    public class CadastrarArmaDetalheRequest
    {
        [Required] public long IdItem { get; set; }
        [Required] public long IdCampanha { get; set; }
        [Required] public string Dano { get; set; }
        [Required] public TipoDanoEnum TipoDano { get; set; }
        public int Alcance { get; set; }
    }

    public class AtualizarArmaDetalheRequest
    {
        public string? Dano { get; set; }
        public TipoDanoEnum TipoDano { get; set; }
        public int? Alcance { get; set; }
    }
}
