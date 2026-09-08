using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace RPGMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagemAtributoController : ControllerBase
    {
        private readonly PersonagemAtributoService _service;

        public PersonagemAtributoController (PersonagemAtributoService service) => _service = service;

        [HttpGet("PersonagemAtributo/Obter")]

        public IActionResult Obter (int idPersonagem)
        {
            var ret = _service.ObterTodos (idPersonagem);
            return ret != null ? Ok (ret) : BadRequest ("Não há nenhum personagem com atributo na campanha");
        }

        [HttpPost("PersonagemAtributo/Adicionar")]
        public IActionResult Adicionar(long idPersonagem, long idAtributo, long idCampanha, int valor)
        {
            var ret = _service.Adicionar(idPersonagem, idAtributo, idCampanha, valor);
            return ret ? Ok("Atributo adicionado ao personagem") : BadRequest("Não foi possivel adicionar o atributo ao personagem");
        }

        [HttpPut("PersonagemAtributo/Editar")]

        public IActionResult Editar (long idPersonagem, long idCampanha, long idAtributo, int valor)
        {
            var ret = _service.Editar(idPersonagem, idAtributo, idCampanha, valor);
            return ret ? Ok("Valor do atributo editado") : BadRequest("Não foi possível editar o atributo");
        }

        [HttpDelete("PersonagemAtributo/Excluir")]

        public IActionResult Excluir (long idPersonagem, long idAtributo, long idCampanha)
        {
            var ret = _service.Excluir(idPersonagem, idAtributo, idCampanha);
            return ret ? Ok("Atributo foi desvinculado com sucesso!") : BadRequest("Não foi possível desvincular o atributo!");
        }
    }
}
