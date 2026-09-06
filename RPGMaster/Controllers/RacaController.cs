using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;

namespace RPGMaster.Controllers
{
    public class RacaController: ControllerBase
    {
        private readonly RacaService _racaService;

        public RacaController(RacaService racaService)
        {
            _racaService = racaService;
        }

        [HttpGet("api/Raça/ObterTodos")]

        public IActionResult ObterTodos(long idCampanha)
        {
            var listaRacas = _racaService.ObterTodos(idCampanha);

            if (listaRacas.Count > 0)
                return Ok(listaRacas);
            else 
                return BadRequest("Não há nenhuma raça nessa campanha");
        }

        [HttpPost("api/Raça/Criar")]
        public IActionResult CriarRaca(long idCampanha, string nome, string descricao)
        {
            var ret = _racaService.CriarRaca(idCampanha, nome, descricao);

            if (!ret)
                return BadRequest("Nõa foi possivel criar a raça");

            return Ok("A raça " +  nome + " foi criada");
        }

        [HttpPut("api/Raça/EditarRaça")]
        public IActionResult EditarRaca (long idRaca, string nome, string descricao, long idCampanha)
        {
            var ret = _racaService.EditarRaca(idRaca, nome, descricao, idCampanha);

            if (!ret)
                return BadRequest("Não foi possivel editar a raça selecionada");

            return Ok("Raça editada com sucesso!");
        }

        [HttpDelete("api/Raça/ExcluirRaça")]
        public IActionResult ExcluirRaca(long idRaca, long idCampanha)
        {
            var ret = _racaService.ExcluirRaca(idRaca, idCampanha);

            if (!ret)
                return BadRequest("Não foi possivel excluir a raça");

            return Ok("Raça excluida!");
        }
    }
}
