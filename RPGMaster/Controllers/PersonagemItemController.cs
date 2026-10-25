using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Model;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace RPGMaster.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagemItemController : ControllerBase
    {
        private readonly PersonagemItemService _service;
        public PersonagemItemController (PersonagemItemService service)
        {
            _service = service;
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos(long idPersonagem)
        {
            var ret = _service.ObterTodos(idPersonagem);

            return ret != null ? Ok(ret) : BadRequest("Não foi possível buscar os itens do personagem!");
        }

        [HttpPost("Adicionar")]
        public IActionResult Adicionar([FromBody] AdicionarItem request)
        {
            var ret = _service.Adicionar(request.idPersonagem, request.idCampanha, request.idItem, request.quantidade);

            return ret ? Ok("Item recebido pelo personagem") : BadRequest("Não foi possível o personagem obter o item"); 
        }

        [HttpPut("Editar")]
        public IActionResult Editar([FromBody] EditarItemRequest request)
        {
            var ret = _service.Editar(request.idPersonagem, request.idItem, request.quant);

            return ret ? Ok("Item editado") : BadRequest("Não foi possível editar o item");
        }

        [HttpDelete("Excluir")]
        public IActionResult Excluir (long idPersonagem, long idItem)
        {
            var ret = _service.Excluir(idPersonagem, idItem);

            return ret ? Ok("O personagem não possui mais esse item") : BadRequest("Não foi possível excluir o item do personagem");
        }       
    }

    public class AdicionarItem
    {
        public long idCampanha { get; set; }
        public long idItem { get; set; }
        public long idPersonagem { get; set; }
        public int quantidade { get; set; }
    }

    public class EditarItemRequest
    {
        public long idPersonagem { get; set; }
        public long idItem { get; set; }
        public int quant { get; set; }
    }
}
