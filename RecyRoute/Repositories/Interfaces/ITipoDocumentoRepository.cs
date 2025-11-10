using RecyRoute.Modelos;

namespace RecyRoute.Repositories.Interfaces
{
    public interface ITipoDocumentoRepository
    {
        Task<List<TipoDocumento>> ObtenerTiposDeDocumento();

        Task<TipoDocumento> ObtenerTipoDocumento(Guid tipodocumento);

        Task<TipoDocumento> CrearTipoDocumento(TipoDocumento tipodocumento);

        Task<TipoDocumento> ActualizarTipoDocumento(TipoDocumento tipodocumento);

        Task<bool> EliminarTipoDocumento(Guid idtipodocumento);
    }
}

