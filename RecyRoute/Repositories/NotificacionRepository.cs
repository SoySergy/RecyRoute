using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly RecyRouteContext _context;

        public NotificacionRepository(RecyRouteContext context)
        {
            _context = context;
        }
        public async Task<List<Notificacion>> ObtenerNotificaciones()
        {
            return await _context.Notificacion
                .Include(n => n.Usuario)
                .Include(n => n.SolicitudRecoleccion)
                .ToListAsync();
        }

        public async Task<Notificacion> ObtenerNotificacion(Guid idNotificacion)
        {
            var notificacion = await _context.Notificacion
                .Include(n => n.Usuario)
                .Include(n => n.SolicitudRecoleccion)
                .FirstOrDefaultAsync(n => n.IdNotificacion == idNotificacion);

            if (notificacion == null)
            {
                throw new KeyNotFoundException($"No se encontró la notificación con ID: {idNotificacion}");
            }

            return notificacion;
        }

        public async Task<Notificacion> CrearNotificacion(Notificacion notificacion)
        {
            _context.Notificacion.Add(notificacion);
            await _context.SaveChangesAsync();
            return notificacion;
        }

        public async Task<Notificacion> ActualizarNotificacion(Notificacion notificacion)
        {
            var notificacionExistente = await _context.Notificacion
                .FirstOrDefaultAsync(n => n.IdNotificacion == notificacion.IdNotificacion);

            if (notificacionExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró la notificación con ID: {notificacion.IdNotificacion}");
            }

            notificacionExistente.Titulo = notificacion.Titulo;
            notificacionExistente.Mensaje = notificacion.Mensaje;
            notificacionExistente.Tipo = notificacion.Tipo;
            notificacionExistente.Leida = notificacion.Leida;

            await _context.SaveChangesAsync();
            return notificacionExistente;
        }

        public async Task<bool> EliminarNotificacion(Guid idNotificacion)
        {
            var notificacion = await _context.Notificacion
                .FirstOrDefaultAsync(n => n.IdNotificacion == idNotificacion);

            if (notificacion == null)
            {
                return false;
            }

            _context.Notificacion.Remove(notificacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
