using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface INotificacionRepository
    {
        Task<List<Notificacion>> ObtenerNotificaciones();
        Task<Notificacion> ObtenerNotificacion(Guid idNotificacion);
        Task<Notificacion> CrearNotificacion(Notificacion notificacion);
        Task<Notificacion> ActualizarNotificacion(Notificacion notificacion);
        Task<bool> EliminarNotificacion(Guid idNotificacion);
    }
}
