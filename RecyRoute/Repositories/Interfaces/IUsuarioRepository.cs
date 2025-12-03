using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ObtenerUsuarios();

        Task<Usuario> ObtenerUsuario(Guid usuario);

        Task<Usuario> CrearUsuario(Usuario usuario);

        Task<Usuario> ActualizarUsuario(Usuario usuario);

        Task<Usuario> ObtenerUsuarioPorCorreo(string correo);

        Task<Usuario> ObtenerUsuarioPorNombreUsuario(string nombreUsuario);

        Task<bool> EliminarUsuario(Guid idusuario);

    }
}
