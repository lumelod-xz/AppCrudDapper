using AppCrudDapper.DTO;
using AppCrudDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppCrudDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if (usuarios.Status == false) 
            { 
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }
        
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> BuscarUsuariosPorId(int usuarioId)
        {
            var usuario = await _usuarioInterface.BuscarUsuariosPorId(usuarioId);

            if (usuario.Status == false)
            {
                return NotFound(usuario);
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)
        {
            var usuario = await _usuarioInterface.CriarUsuario(usuarioCriarDTO);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO)
        {
            var usuario = await _usuarioInterface.EditarUsuario(usuarioEditarDTO);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuario = await _usuarioInterface.RemoverUsuario(usuarioId);

            if (usuario.Status == false)
            {
                return NotFound(usuario);
            }

            return Ok(usuario);
        }
    }
}
