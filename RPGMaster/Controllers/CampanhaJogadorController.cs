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
    }
}
