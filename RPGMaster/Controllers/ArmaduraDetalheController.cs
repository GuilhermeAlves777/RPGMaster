using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArmaduraDetalheController : ControllerBase
    {
        private readonly ArmaduraDetalheService _armaduraDetalheService;

        public ArmaduraDetalheController(ArmaduraDetalheService armaduraDetalheService) => _armaduraDetalheService = armaduraDetalheService;

        [HttpGet("item/{idItem}")]
        public IActionResult ObterPorItem(long idItem) =>
            Ok(_armaduraDetalheService.ObterPorItem(idItem));

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CadastrarArmaduraDetalheRequest request)
        {
            var ret = _armaduraDetalheService.Cadastrar(request.IdItem, request.IdCampanha, request.Defesa);
            return ret ? Ok("Detalhes de armadura cadastrados!") : BadRequest("Não foi possível cadastrar");
        }

        [HttpPut("{idItem}")]
        public IActionResult Atualizar(long idItem, [FromBody] AtualizarArmaduraDetalheRequest request)
        {
            var ret = _armaduraDetalheService.Atualizar(idItem, request.Defesa);
            return ret ? Ok("Detalhes de armadura atualizados!") : BadRequest("Não foi possível atualizar");
        }

        [HttpDelete("{idItem}")]
        public IActionResult Deletar(long idItem)
        {
            var ret = _armaduraDetalheService.Deletar(idItem);
            return ret ? Ok("Detalhes de armadura removidos!") : BadRequest("Não foi possível remover");
        }
    }

    public class CadastrarArmaduraDetalheRequest
    {
        [Required] public long IdItem { get; set; }
        [Required] public long IdCampanha { get; set; }
        public int Defesa { get; set; }
    }

    public class AtualizarArmaduraDetalheRequest
    {
        public int Defesa { get; set; }
    }
}
