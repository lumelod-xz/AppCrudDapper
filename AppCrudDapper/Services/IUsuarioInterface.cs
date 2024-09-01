using AppCrudDapper.DTO;
using AppCrudDapper.Models;

namespace AppCrudDapper.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListarDTO>>> BuscarUsuarios();
        Task<ResponseModel<UsuarioListarDTO>> BuscarUsuariosPorId(int usuarioId);
        Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO);
        Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO);
        Task<ResponseModel<List<UsuarioListarDTO>>> RemoverUsuario(int usuarioId);

    }
}
