using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface IGestionRecoleccionRepository
    {
        Task<List<GestionRecoleccion>> ObtenerGestionesRecoleccion();
        Task<GestionRecoleccion> ObtenerGestionRecoleccion(Guid idGestion);
        Task<GestionRecoleccion> CrearGestionRecoleccion(GestionRecoleccion gestionRecoleccion);
        Task<GestionRecoleccion> ActualizarGestionRecoleccion(GestionRecoleccion gestionRecoleccion);
        Task<bool> EliminarGestionRecoleccion(Guid idGestion);
    }
}
