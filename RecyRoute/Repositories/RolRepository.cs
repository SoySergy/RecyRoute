using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class RolRepository : IRolRepository
    {

        private readonly RecyRouteContext _context;

        public RolRepository(RecyRouteContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ObtenerRoles()
        {
            return await _context.Rol.ToListAsync();
        }

        public async Task<Rol> ObtenerRol(Guid idrol)
        {
            return await _context.Rol.FirstOrDefaultAsync(r => r.IdRol == idrol);
        }

        public async Task<Rol> CrearRol(Rol rol)
        {
            _context.Rol.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<Rol> ActualizarRol(Rol rol)
        {
            _context.Rol.Update(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<bool> EliminarRol(Guid idrol)
        {
            var rol = await _context.Rol.FirstOrDefaultAsync(r => r.IdRol == idrol);
            if (rol == null)
                return false;

            _context.Rol.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
