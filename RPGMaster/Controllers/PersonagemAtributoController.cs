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

        [HttpGet("ObterTodos")]

        public IActionResult Obter (int idPersonagem)
        {
            var ret = _service.ObterTodos (idPersonagem);
            return ret != null ? Ok (ret) : BadRequest ("Não há nenhum personagem com atributo na campanha");
        }

        [HttpPost("Adicionar")]
        public IActionResult Adicionar(long idPersonagem, long idAtributo, long idCampanha, int valor)
        {
            var ret = _service.Adicionar(idPersonagem, idAtributo, idCampanha, valor);
            return ret ? Ok("Atributo adicionado ao personagem") : BadRequest("Não foi possivel adicionar o atributo ao personagem");
        }

        [HttpPut("Editar")]

        public IActionResult Editar([FromBody] Editar request )
        {
            var ret = _service.Editar(request.idPersonagem, request.idCampanha, request.idAtributo, request.valor);
            return ret ? Ok("Valor do atributo editado") : BadRequest("Não foi possível editar o atributo");
        }

        [HttpDelete("Excluir")]

        public IActionResult Excluir (long idPersonagem, long idAtributo, long idCampanha)
        {
            var ret = _service.Excluir(idPersonagem, idAtributo, idCampanha);
            return ret ? Ok("Atributo foi desvinculado com sucesso!") : BadRequest("Não foi possível desvincular o atributo!");
        }
    }

    public class Editar
    {
        public long idAtributo { get; set; }
        public long idCampanha { get; set; }
        public long idPersonagem { get; set; }
        public int valor { get; set; }
    }
}
