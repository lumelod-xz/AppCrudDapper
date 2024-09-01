using AppCrudDapper.DTO;
using AppCrudDapper.Models;
using AutoMapper;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppCrudDapper.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        public readonly IConfiguration _configuration;
        public readonly IMapper _mapper;
        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<UsuarioListarDTO>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryAsync<Usuario>("Select * from Usuarios");

                if (usuariosBanco.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário localizado";
                    response.Status = false;
                    return response;
                }

                //Transformação Mapper
                var usuarioMapeado = _mapper.Map<List<UsuarioListarDTO>>(usuariosBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuários localizados com sucesso";
            }
            return response;
        }

        public async Task<ResponseModel<UsuarioListarDTO>> BuscarUsuariosPorId(int usuarioId)
        {
            ResponseModel<UsuarioListarDTO> response = new ResponseModel<UsuarioListarDTO>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryFirstOrDefaultAsync<Usuario>
                    ("Select * from Usuarios where id = @Id", new { Id = usuarioId});

                if (usuariosBanco == null)
                {
                    response.Mensagem = $"Usuário com Id: {usuarioId} não localizado";
                    response.Status = false;
                    return response;
                }

                //Transformação Mapper
                var usuarioMapeado = _mapper.Map<UsuarioListarDTO>(usuariosBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuários localizados com sucesso";
            }
            return response;

        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync
                    ("INSERT INTO Usuarios" +
                     "(NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao)" +
                     "VALUES(@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", usuarioCriarDTO);

                if (usuariosBanco == 0)
                {
                    response.Mensagem = $"Ocorreu um erro ao criar o usuário";
                    response.Status = false;
                    return response;
                }

                //Transformação Mapper
                var usuario = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>> (usuario);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso";
            }
            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync
                    ("UPDATE Usuarios SET" +
                     "  NomeCompleto = @NomeCompleto" +
                     ", Email = @Email, Cargo = @Cargo, Salario = @Salario" +
                     ", Situacao = @Situacao, CPF = @CPF WHERE Id = @Id", usuarioEditarDTO);

                if (usuariosBanco == 0)
                {
                    response.Mensagem = $"Ocorreu um erro ao realizar a edição do usuário";
                    response.Status = false;
                    return response;
                }

                //Transformação Mapper
                var usuario = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuario);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso";
            }
            return response;
        }

        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("Select * from Usuarios");
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync
                    ("DELETE FROM Usuarios WHERE Id = @Id", new { Id = usuarioId });

                if (usuariosBanco == 0)
                {
                    response.Mensagem = $"Ocorreu um erro a remoção usuário";
                    response.Status = false;
                    return response;
                }

                //Transformação Mapper
                var usuario = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuario);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso";
            }
            return response;
        }
    }
}
