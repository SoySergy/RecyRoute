using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly RecyRouteContext _context;

        public UsuarioRepository(RecyRouteContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            return await _context.Usuario
                                 .Include(u => u.Rol)
                                 .Include(u => u.TipoDocumento)
                                 .ToListAsync();
        }

        public async Task<Usuario> ObtenerUsuario(Guid usuario)
        {
            return await _context.Usuario
                                 .Include(u => u.Rol)
                                 .Include(u => u.TipoDocumento)
                                 .FirstOrDefaultAsync(u => u.IdUsuario == usuario);
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> EliminarUsuario(Guid idusuario)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == idusuario);
            if (usuario == null)
                return false;

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            return await _context.Usuario
                .Include(u => u.Rol)
                .Include(u => u.TipoDocumento)
                .FirstOrDefaultAsync(u => u.Nombre == nombreUsuario);
        }
        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
        {
            return await _context.Usuario
                .Include(u => u.Rol)
                .Include(u => u.TipoDocumento)
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}
