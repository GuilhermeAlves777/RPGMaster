using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Model.DTOs;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace RPGMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemAtributoController : ControllerBase
    {
        private readonly ItemAtributoService _itemAtributoService;

        public ItemAtributoController(ItemAtributoService itemAtributoService) => _itemAtributoService = itemAtributoService;

        [HttpGet("item/{idItem}")]
        public IActionResult ObterTodos(long idItem) =>
            Ok(_itemAtributoService.ObterTodos(idItem));

        [HttpPost]
        public IActionResult Adicionar([FromBody] AdicionarItemAtributoRequest request)
        {
            var ret = _itemAtributoService.Adicionar(request.IdItem, request.IdAtributo, request.IdCampanha, request.Modificador);
            return ret ? Ok("Modificador adicionado ao item!") : BadRequest("Não foi possível adicionar o modificador");
        }

        [HttpPut]
        public IActionResult Editar([FromBody] EditarItemAtributoRequest request)
        {
            var ret = _itemAtributoService.Editar(request.IdItem, request.IdCampanha, request.IdAtributo, request.Modificador);
            return ret ? Ok("Modificador atualizado!") : BadRequest("Não foi possível atualizar o modificador");
        }

        [HttpDelete("{idItem}/{idAtributo}")]
        public IActionResult Excluir(long idItem, long idAtributo)
        {
            var ret = _itemAtributoService.Excluir(idItem, idAtributo);
            return ret ? Ok("Modificador removido!") : BadRequest("Não foi possível remover o modificador");
        }
    }

    public class AdicionarItemAtributoRequest
    {
        [Required] public long IdItem { get; set; }
        [Required] public long IdAtributo { get; set; }
        [Required] public long IdCampanha { get; set; }
        public int Modificador { get; set; }
    }

    public class EditarItemAtributoRequest
    {
        [Required] public long IdItem { get; set; }
        [Required] public long IdCampanha { get; set; }
        [Required] public long IdAtributo { get; set; }
        public int Modificador { get; set; }
    }
}
