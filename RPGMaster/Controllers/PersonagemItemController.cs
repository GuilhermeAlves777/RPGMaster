using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Model;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace RPGMaster.Controllers
{
    [Authorize]
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
        public IActionResult Adicionar (long idPersonagem, long idCampanha, long idItem, int quant)
        {
            var ret = _service.Adicionar(idPersonagem, idCampanha, idItem, quant);

            return ret ? Ok("Item recebido pelo personagem") : BadRequest("Não foi possível o personagem obter o item"); 
        }

        [HttpDelete("Excluir")]
        public IActionResult Excluir (long idPersonagem, long idItem)
        {
            var ret = _service.Excluir(idPersonagem, idItem);

            return ret ? Ok("O personagem não possui mais esse item") : BadRequest("Não foi possível excluir o item do personagem");
        }       
    }
}
