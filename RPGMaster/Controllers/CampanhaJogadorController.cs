using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Service;

namespace RPGMaster.Controllers
{
    public class CampanhaJogadorController : ControllerBase
    {
        private readonly CampanhaJogadorService _campanhaJogadorService;

        public CampanhaJogadorController(CampanhaJogadorService campanhaJogadorService)
        {
            _campanhaJogadorService = campanhaJogadorService;
        }

        [HttpGet("api/CampanhaJogador/ObterTodos")]
        public IActionResult ObterTodos()
        {
            var ret = _campanhaJogadorService.ObterTodos();

            if (ret == null)
                return BadRequest("Não foi possivel buscar todos os jogadores");

            return Ok(ret);
        }
        [HttpGet("api/CampanhaJogador/ObterJogadorPorCampanha")]
        public IActionResult ObterJogadrPorCampanha(long idCampanha)
        {
            var ret = _campanhaJogadorService.ObterJogadorPorCampanha(idCampanha);

            if (ret == null)
                return BadRequest("Não há nenhum jogador na campanha");

            return Ok(ret);
        }

        [Authorize]
        [HttpPost("{idCampanha}/jogadores/{user}")]
        public IActionResult AdicionarJogador(long idCampanha, string user)
        {
            try
            {
                var ret = _campanhaJogadorService.AdicionarJogador(idCampanha, user);
                return Ok("Jogador adicionado a campanha");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("api/CampanhaJogador/Excluir")]

        public IActionResult ExcluirJogador(long idCampanha, long idUsuario)
        {
            var ret = _campanhaJogadorService.ExcluirJogador(idCampanha, idUsuario);

            if (!ret)
                return BadRequest("Não foi possivel excluir o jogador da campanha");

            return Ok("Jogador excluido da campanha");
        }
    }
}
