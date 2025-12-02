using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class HistorialChatRepository : IHistorialChatRepository
    {
        private readonly RecyRouteContext _context;

        public HistorialChatRepository(RecyRouteContext context)
        {
            _context = context;
        }

        public async Task<List<HistorialChat>> ObtenerMensajes()
        {
            return await _context.HistorialChat
                                 .Include(h => h.SolicitudRecoleccion)
                                 .Include(h => h.Emisor)
                                 .OrderBy(h => h.FechaEnvio)
                                 .ToListAsync();
        }

        public async Task<List<HistorialChat>> ObtenerMensajesPorSolicitud(Guid idSolicitud)
        {
            return await _context.HistorialChat
                                 .Include(h => h.Emisor)
                                 .Where(h => h.IdSolicitud == idSolicitud)
                                 .OrderBy(h => h.FechaEnvio)
                                 .ToListAsync();
        }

        public async Task<HistorialChat> ObtenerMensaje(Guid idHistorialChat)
        {
            return await _context.HistorialChat
                                 .Include(h => h.SolicitudRecoleccion)
                                 .Include(h => h.Emisor)
                                 .FirstOrDefaultAsync(h => h.IdHistorialChat == idHistorialChat);
        }

        public async Task<HistorialChat> CrearMensaje(HistorialChat historialChat)
        {
            _context.HistorialChat.Add(historialChat);
            await _context.SaveChangesAsync();
            return historialChat;
        }

        public async Task<HistorialChat> ActualizarMensaje(HistorialChat historialChat)
        {
            _context.HistorialChat.Update(historialChat);
            await _context.SaveChangesAsync();
            return historialChat;
        }

        public async Task<bool> EliminarMensaje(Guid idHistorialChat)
        {
            var mensaje = await _context.HistorialChat.FirstOrDefaultAsync(h => h.IdHistorialChat == idHistorialChat);
            if (mensaje == null)
                return false;

            _context.HistorialChat.Remove(mensaje);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarcarComoLeido(Guid idHistorialChat)
        {
            var mensaje = await _context.HistorialChat.FirstOrDefaultAsync(h => h.IdHistorialChat == idHistorialChat);
            if (mensaje == null)
                return false;

            mensaje.Leido = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HistorialChat>> ObtenerMensajesNoLeidos(Guid idUsuario, Guid idSolicitud)
        {
            return await _context.HistorialChat
                                 .Include(h => h.Emisor)
                                 .Where(h => h.IdSolicitud == idSolicitud
                                          && !h.Leido
                                          && h.IdEmisor != idUsuario)
                                 .OrderBy(h => h.FechaEnvio)
                                 .ToListAsync();
        }
    }
}