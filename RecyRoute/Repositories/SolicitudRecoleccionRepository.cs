using Microsoft.EntityFrameworkCore;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class SolicitudRecoleccionRepository : ISolicitudRecoleccionRepository
    {
        private readonly RecyRouteContext _context;

        public SolicitudRecoleccionRepository(RecyRouteContext context)
        {
            _context = context;
        }

        public async Task<List<SolicitudRecoleccion>> ObtenerSolicitudesRecoleccion()
        {
            return await _context.SolicitudRecoleccion
                                 .Include(s => s.Usuario)
                                 .ToListAsync();
        }

        public async Task<SolicitudRecoleccion> ObtenerSolicitudRecoleccion(Guid idSolicitud)
        {
            return await _context.SolicitudRecoleccion
                                 .Include(s => s.Usuario)
                                 .FirstOrDefaultAsync(s => s.IdSolicitud == idSolicitud);
        }

        public async Task<SolicitudRecoleccion> CrearSolicitudRecoleccion(SolicitudRecoleccion solicitudRecoleccion)
        {
            _context.SolicitudRecoleccion.Add(solicitudRecoleccion);
            await _context.SaveChangesAsync();
            return solicitudRecoleccion;
        }

        public async Task<SolicitudRecoleccion> ActualizarSolicitudRecoleccion(SolicitudRecoleccion solicitudRecoleccion)
        {
            _context.SolicitudRecoleccion.Update(solicitudRecoleccion);
            await _context.SaveChangesAsync();
            return solicitudRecoleccion;
        }

        public async Task<bool> EliminarSolicitudRecoleccion(Guid idSolicitud)
        {
            var solicitudRecoleccion = await _context.SolicitudRecoleccion
                                                     .FirstOrDefaultAsync(s => s.IdSolicitud == idSolicitud);
            if (solicitudRecoleccion == null)
                return false;

            _context.SolicitudRecoleccion.Remove(solicitudRecoleccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
