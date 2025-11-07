using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface IRolRepository
    {
        Task<List<Rol>> ObtenerRoles();

        Task<Rol> ObtenerRol (Guid rol);

        Task<Rol> CrearRol (Rol rol);

        Task<Rol> ActualizarRol (Rol rol);

        Task<bool> EliminarRol (Guid idrol);
    }
}



