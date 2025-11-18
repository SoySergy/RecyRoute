using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class GestionRecoleccionRepository : IGestionRecoleccionRepository
    {
        private readonly RecyRouteContext _context;

        public GestionRecoleccionRepository(RecyRouteContext context)
        {
            _context = context;
        }

        public async Task<List<GestionRecoleccion>> ObtenerGestionesRecoleccion()
        {
            return await _context.GestionRecoleccion
                                 .Include(g => g.SolicitudRecoleccion)
                                 .Include(g => g.Gestor)
                                 .ToListAsync();
        }

        public async Task<GestionRecoleccion> ObtenerGestionRecoleccion(Guid idGestion)
        {
            return await _context.GestionRecoleccion
                                 .Include(g => g.SolicitudRecoleccion)
                                 .Include(g => g.Gestor)
                                 .FirstOrDefaultAsync(g => g.IdGestion == idGestion);
        }

        public async Task<GestionRecoleccion> CrearGestionRecoleccion(GestionRecoleccion gestionRecoleccion)
        {
            _context.GestionRecoleccion.Add(gestionRecoleccion);
            await _context.SaveChangesAsync();
            return gestionRecoleccion;
        }

        public async Task<GestionRecoleccion> ActualizarGestionRecoleccion(GestionRecoleccion gestionRecoleccion)
        {
            _context.GestionRecoleccion.Update(gestionRecoleccion);
            await _context.SaveChangesAsync();
            return gestionRecoleccion;
        }

        public async Task<bool> EliminarGestionRecoleccion(Guid idGestion)
        {
            var gestionRecoleccion = await _context.GestionRecoleccion
                                                   .FirstOrDefaultAsync(g => g.IdGestion == idGestion);
            if (gestionRecoleccion == null)
                return false;

            _context.GestionRecoleccion.Remove(gestionRecoleccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
