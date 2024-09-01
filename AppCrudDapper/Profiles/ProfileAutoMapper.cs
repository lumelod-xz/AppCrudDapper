using AppCrudDapper.DTO;
using AppCrudDapper.Models;
using AutoMapper;

namespace AppCrudDapper.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, UsuarioListarDTO>();
            CreateMap<UsuarioCriarDTO, Usuario>();
        }
    }
}
