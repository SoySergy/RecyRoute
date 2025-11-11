using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RecyRoute.Context;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Repositories
{
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {

        private readonly RecyRouteContext _context;

        public TipoDocumentoRepository(RecyRouteContext context)
        {
            _context = context;
        }

        public async Task<List<TipoDocumento>> ObtenerTiposDeDocumento()
        {
            return await _context.TipoDocumento.ToListAsync();
        }

        public async Task<TipoDocumento> ObtenerTipoDocumento(Guid tipodocumento)
        {
            return await _context.TipoDocumento.FirstOrDefaultAsync(t => t.IdTipoDocumento == tipodocumento);
        }

        public async Task<TipoDocumento> CrearTipoDocumento(TipoDocumento tipodocumento)
        {
            _context.TipoDocumento.Add(tipodocumento);
            await _context.SaveChangesAsync();
            return tipodocumento;
        }

        public async Task<TipoDocumento> ActualizarTipoDocumento(TipoDocumento tipodocumento)
        {
            _context.TipoDocumento.Update(tipodocumento);
            await _context.SaveChangesAsync();
            return tipodocumento;
        }

        public async Task<bool> EliminarTipoDocumento(Guid idtipodocumento)
        {
            var tipoDoc = await _context.TipoDocumento.FirstOrDefaultAsync(t => t.IdTipoDocumento == idtipodocumento);
            if (tipoDoc == null)
                return false;

            _context.TipoDocumento.Remove(tipoDoc);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
