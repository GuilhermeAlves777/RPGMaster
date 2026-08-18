using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RPGMaster.Service;
using RPGMaster.Model.DTOs;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace RPGMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterUsuarios()
        {
            var listaClientes = _usuarioService.ObterTodos();
            return Ok(listaClientes);
        }

        [HttpGet("ObterPorId")]
        public IActionResult ObterUsuarioPorID(long id)
        {
            var usu = _usuarioService.ObterPorId(id);
            return Ok(usu);
        }

        [HttpPost("Criar")]
        public IActionResult CadastrarUsuarios(string nome, string sobrenome, string user, string senha, string email, string cpf, DateTime Dn)
        {
            var result = _usuarioService.CadastrarUsuario(nome, sobrenome, user, senha, email, cpf, Dn);
            if (result)
                return Ok("Usuario cadastrado com sucesso");
            else
                return BadRequest("Erro ao cadastrar o Usuario");
        }

        [HttpPut("Atualizar")]

        public IActionResult AtualizarUsuario(long id, string nome, string sobrenome, string user, string email, int empresaId, string cpf, DateTime Dn)
        {
            var result = _usuarioService.AtualizarUsuario(id, nome, sobrenome, user, email, empresaId, cpf, Dn);
            if (result)
                return Ok("Usuario atualizado com sucesso");

            return BadRequest("Houve um erro na atualização do usuário");

        }

        [HttpPost("Login")]

        public IActionResult LoginUsuario([FromBody] LoginDto dto)
        {
            var result = _usuarioService.LoginUsuario(
                dto.User,
                dto.Senha);

            //if (result)
            //    return Ok("Login Feito");
            //else
            //    return Unauthorized("Usuário ou senha incorretos");

            if (result)
            {
                var usuario = _usuarioService.ObterPorUser(dto.User);
                if (usuario == null) return Unauthorized();

                var token = TokenService.GenerateToken(usuario);
                return Ok(token);
            }

            return Unauthorized("Usuario ou senha invalidos");

        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult ObterUsuarioLogado()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(idClaim, out var id))
            {
                return Unauthorized();
            }

            var usuario = _usuarioService.ObterPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpDelete("Deletar")]

        public IActionResult ExcluirUsuario(long id)
        {
            var result = _usuarioService.ExcluirUsuario(id);

            if (result)
                return Ok("Usuário deletado com sucesso");

            return BadRequest("Houve um erro!");
        }


    }
    
}
