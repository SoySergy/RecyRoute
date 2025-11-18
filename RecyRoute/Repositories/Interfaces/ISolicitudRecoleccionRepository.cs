using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface ISolicitudRecoleccionRepository
    {
        Task<List<SolicitudRecoleccion>> ObtenerSolicitudesRecoleccion();
        Task<SolicitudRecoleccion> ObtenerSolicitudRecoleccion(Guid idSolicitud);
        Task<SolicitudRecoleccion> CrearSolicitudRecoleccion(SolicitudRecoleccion solicitudRecoleccion);
        Task<SolicitudRecoleccion> ActualizarSolicitudRecoleccion(SolicitudRecoleccion solicitudRecoleccion);
        Task<bool> EliminarSolicitudRecoleccion(Guid idSolicitud);
    }
}
