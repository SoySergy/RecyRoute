using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface IHistorialChatRepository
    {
        Task<List<HistorialChat>> ObtenerMensajes();
        Task<List<HistorialChat>> ObtenerMensajesPorSolicitud(Guid idSolicitud);
        Task<HistorialChat> ObtenerMensaje(Guid idHistorialChat);
        Task<HistorialChat> CrearMensaje(HistorialChat historialChat);
        Task<HistorialChat> ActualizarMensaje(HistorialChat historialChat);
        Task<bool> EliminarMensaje(Guid idHistorialChat);
        Task<bool> MarcarComoLeido(Guid idHistorialChat);
        Task<List<HistorialChat>> ObtenerMensajesNoLeidos(Guid idUsuario, Guid idSolicitud);
    }
}
