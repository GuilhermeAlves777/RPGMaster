using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGMaster.Model.Enums;
using RPGMaster.Service;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace RPGMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConviteController : ControllerBase
    {
        private readonly ConviteService _conviteService;

        public ConviteController(ConviteService conviteService)
        {
            _conviteService = conviteService;
        }

        [HttpPost("GerarConvite")]

        public IActionResult GerarConvite (long idCampanha)
        {
            var result = _conviteService.GerarConvite(idCampanha);

            if (result == null) BadRequest("Não foi possível gerar o convite");

            return Ok(result);
        }

        [HttpGet("ObterAtivo")]

        public IActionResult ObterConviteAtivo (long idCampanha)
        {
            var result = _conviteService.ObterConvite(idCampanha);

            if (result == null) BadRequest("Não há nenhum convite ativo nessa campanha");

            return Ok(result);
        }

        [HttpGet("ObterConvite")]

        public IActionResult ObterConvite (string Codigo)
        {
            var result = _conviteService.ObterConvite(Codigo);

            if (result != null) BadRequest("Esse convite não existe");

            return Ok(result);
        }

        [HttpPost("AceiteConvite")]

        public IActionResult AceiteConvite (string Codigo)
        {
            var result = _conviteService.AceiteConvite(Codigo);

            if(!result) BadRequest("Esse convite não existe");

            return Ok("Convite aceito");
        }
    }
}
