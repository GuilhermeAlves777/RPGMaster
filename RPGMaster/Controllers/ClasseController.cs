using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;

namespace RPGMaster.Controllers
{
    public class ClasseController : ControllerBase
    {
        private readonly ClasseService _classeService;

        public ClasseController (ClasseService classeService)
        {
            _classeService = classeService;
        }

        [HttpGet("api/Classe/ObterTodos")]

        public IActionResult ObterTodos (long id)
        {
            var ret = _classeService.ObterTodos(id);

            if (ret == null) return BadRequest("Não há nenhuma classe registrada na campanha");

            return Ok(ret);
        }

        [HttpGet("api/Classe/ObterPorId")]
        public IActionResult ObterPorId (long idClasse, long idCampanha)
        {
            var ret = _classeService.ObterPorId(idClasse, idCampanha);

            if (ret == null) return BadRequest("Não há nenhuma classe registrada na campanha com esse ID");

            return Ok(ret);
        }

        [HttpPost("api/Classe/Adicionar")]

        public IActionResult Adicionar (long idCampanha, string nome, string descricao)
        {
            var ret = _classeService.AdicionarClasse(idCampanha, nome, descricao);

            if (!ret) return BadRequest("Não foi possivel adicionar a classe");

            return Ok("A classe " + nome + " foi criada");
        }

        [HttpPut("api/Classe/Editar")]

        public IActionResult Editar (long idClasse, long idCampanha, string nome, string descricao) 
        {
            var ret = _classeService.EditarClasse(idClasse, idCampanha, nome, descricao);

            if (!ret) return BadRequest("Não foi possível editar a classe");

            return Ok("Classe editada com sucesso!");
        }

        [HttpDelete("api/Classe/Excluir")]

        public IActionResult Excluir (long idClasse, long idCampanha)
        {
            var ret = _classeService.ExcluirClasse(idClasse, idCampanha);

            if (!ret) return BadRequest("Não foi possivel excluir a classe");

            return Ok("A classe foi excluída");
        }
    }
}
