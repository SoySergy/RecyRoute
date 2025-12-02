using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface IHistorialRepository
    {
        Task<IEnumerable<Historial>> ObtenerTodosAsync();
        Task<Historial?> ObtenerPorId(Guid idHistorial);
        Task<IEnumerable<Historial>> ObtenerPorSolicitud(Guid idSolicitud);
        Task<IEnumerable<Historial>> ObtenerPorUsuario(Guid idUsuario);
        Task<Historial> Crear(Historial historial);
        Task<Historial?> Actualizar(Historial historial);
        Task<bool> Eliminar(Guid idHistorial);
        Task<bool> Existe(Guid idHistorial);
        Task<IEnumerable<Historial>> ObtenerPorFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Historial>> ObtenerPorEstadoNuevo(string estadoNuevo);
    }
}
