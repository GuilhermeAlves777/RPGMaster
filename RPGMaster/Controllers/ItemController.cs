using Microsoft.AspNetCore.Mvc;
using RPGMaster.Model.Enums;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;

namespace RPGMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService) => _itemService = itemService;

        [HttpGet("campanha/{idCampanha}")]
        public IActionResult ObterPorCampanha(long idCampanha) =>
            Ok(_itemService.ObterPorCampanha(idCampanha));

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CadastrarItemRequest request)
        {
            var ret = _itemService.Cadastrar(request.Nome, request.Tipo, request.Descricao, request.Imagem, request.IdCampanha);
            return ret ? Ok("Item cadastrado!") : BadRequest("Não foi possível cadastrar o item");
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(long id, [FromBody] AtualizarItemRequest request)
        {
            var ret = _itemService.Atualizar(id, request.Nome, request.Tipo, request.Descricao, request.Imagem);
            return ret ? Ok("Item atualizado!") : BadRequest("Não foi possível atualizar o item");
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(long id)
        {
            var ret = _itemService.Deletar(id);
            return ret ? Ok("Item removido!") : BadRequest("Não foi possível remover o item");
        }
    }

    public class CadastrarItemRequest
    {
        [Required] public string? Nome { get; set; }
        [Required] public TiposEnum Tipo { get; set; }
        public string? Descricao { get; set; }
        public string? Imagem { get; set; }
        [Required] public long IdCampanha { get; set; }
    }

    public class AtualizarItemRequest
    {
        public string? Nome { get; set; }
        public TiposEnum Tipo { get; set; }
        public string? Descricao { get; set; }
        public string? Imagem { get; set; }
    }
}
