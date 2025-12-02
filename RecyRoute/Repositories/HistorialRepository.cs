using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class HistorialRepository : IHistorialRepository
    {
        private readonly RecyRouteContext _context;

        public HistorialRepository(RecyRouteContext context)
        {
            _context = context;
        }

        // Obtiene todos los registros del historial con sus relaciones de solicitud y usuario
        // Ordena por fecha de cambio descendente (más recientes primero)
        public async Task<IEnumerable<Historial>> ObtenerTodos()
        {
            return await _context.Historial
                .Include(h => h.SolicitudRecoleccion)
                .Include(h => h.Usuario)
                .OrderByDescending(h => h.FechaCambio)
                .ToListAsync();
        }

        // Busca un registro específico del historial por su ID
        // Incluye las relaciones de solicitud y usuario
        public async Task<Historial?> ObtenerPorId(Guid idHistorial)
        {
            return await _context.Historial
                .Include(h => h.SolicitudRecoleccion)
                .Include(h => h.Usuario)
                .FirstOrDefaultAsync(h => h.IdHistorial == idHistorial);
        }

        // Obtiene todo el historial de cambios de una solicitud específica
        // Útil para rastrear todos los estados por los que ha pasado una solicitud
        public async Task<IEnumerable<Historial>> ObtenerPorSolicitud(Guid idSolicitud)
        {
            return await _context.Historial
                .Include(h => h.Usuario)
                .Where(h => h.IdSolicitud == idSolicitud)
                .OrderByDescending(h => h.FechaCambio)
                .ToListAsync();
        }

        // Obtiene todos los cambios realizados por un usuario específico
        // Útil para auditoría y seguimiento de acciones de usuarios
        public async Task<IEnumerable<Historial>> ObtenerPorUsuario(Guid idUsuario)
        {
            return await _context.Historial
                .Include(h => h.SolicitudRecoleccion)
                .Where(h => h.IdUsuario == idUsuario)
                .OrderByDescending(h => h.FechaCambio)
                .ToListAsync();
        }

        // Crea un nuevo registro en el historial
        // Establece automáticamente la fecha de cambio al momento actual
        public async Task<Historial> Crear(Historial historial)
        {
            historial.FechaCambio = DateTime.Now;
            _context.Historial.Add(historial);
            await _context.SaveChangesAsync();
            return historial;
        }

        // Actualiza un registro existente del historial
        // Solo permite modificar estados y comentarios, no la fecha ni las relaciones
        public async Task<Historial?> Actualizar(Historial historial)
        {
            _context.Historial.Update(historial);
            await _context.SaveChangesAsync();
            return historial;
        }

        // Elimina un registro del historial de la base de datos
        // Retorna true si se eliminó correctamente, false si no existe
        public async Task<bool> Eliminar(Guid idHistorial)
        {
            var historial = await _context.Historial.FirstOrDefaultAsync(h => h.IdHistorial == idHistorial);

            if (historial == null)
                return false;

            _context.Historial.Remove(historial);
            await _context.SaveChangesAsync();
            return true;
        }

        // Verifica si existe un registro de historial con el ID proporcionado
        // Útil para validaciones antes de realizar operaciones
        public async Task<bool> Existe(Guid idHistorial)
        {
            return await _context.Historial
                .AnyAsync(h => h.IdHistorial == idHistorial);
        }

        // Obtiene registros del historial dentro de un rango de fechas
        // Útil para reportes y análisis de cambios en períodos específicos
        public async Task<IEnumerable<Historial>> ObtenerPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Historial
                .Include(h => h.SolicitudRecoleccion)
                .Include(h => h.Usuario)
                .Where(h => h.FechaCambio >= fechaInicio && h.FechaCambio <= fechaFin)
                .OrderByDescending(h => h.FechaCambio)
                .ToListAsync();
        }

        // Filtra el historial por el estado nuevo al que cambiaron
        // Útil para encontrar todas las veces que las solicitudes pasaron a un estado específico
        public async Task<IEnumerable<Historial>> ObtenerPorEstadoNuevo(string estadoNuevo)
        {
            return await _context.Historial
                .Include(h => h.SolicitudRecoleccion)
                .Include(h => h.Usuario)
                .Where(h => h.EstadoNuevo == estadoNuevo)
                .OrderByDescending(h => h.FechaCambio)
                .ToListAsync();
        }

        public Task<IEnumerable<Historial>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
