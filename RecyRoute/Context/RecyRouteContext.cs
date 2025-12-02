using RecyRoute.Modelos;
using Microsoft.EntityFrameworkCore;

namespace RecyRoute.Context
{
    public class RecyRouteContext : DbContext
    {
        public RecyRouteContext(DbContextOptions<RecyRouteContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<SolicitudRecoleccion> SolicitudRecoleccion { get; set; }
        public DbSet<GestionRecoleccion> GestionRecoleccion { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<Historial> Historial { get; set; }
        public DbSet<HistorialChat> HistorialChat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Modelo: Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Define la clave primaria de la entidad Usuario
                entity.HasKey(e => e.IdUsuario);

                // Configura la propiedad IdUsuario como nombre de columna en la base de datos
                entity.Property(e => e.IdUsuario).HasColumnName("IdUsuario");

                // Define que IdRol es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdRol).IsRequired().HasColumnName("IdRol");

                // Define que IdTipoDocumento es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdTipoDocumento).IsRequired().HasColumnName("IdTipoDocumento");

                // Configura la columna Nombre, requerida y con longitud máxima 70
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(70).HasColumnName("Nombre");

                // Configura la columna Apellido, requerida y con longitud máxima 70
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(70).HasColumnName("Apellido");

                // Configura la columna Correo, requerida y con longitud máxima 100
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100).HasColumnName("Correo");

                // Configura la columna Contrasena, requerida y con longitud máxima 200
                entity.Property(e => e.Contrasena).IsRequired().HasMaxLength(200).HasColumnName("Contrasena");

                // Configura la columna NumeroDeTelefono, requerida y con longitud máxima 20
                entity.Property(e => e.NumeroDeTelefono).IsRequired().HasMaxLength(20).HasColumnName("NumeroDeTelefono");

                // Configura la columna Direccion, requerida y con longitud máxima 200
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(200).HasColumnName("Direccion");

                // Configura la columna FechaRegistro con valor por defecto de la fecha actual
                entity.Property(e => e.FechaRegistro).IsRequired().HasColumnName("FechaRegistro").HasDefaultValueSql("GETDATE()"); // función SQL que inserta la fecha del servidor

                // Relación: un Usuario pertenece a un Rol
                // HasOne = relación uno a uno o uno a muchos desde Usuario hacia Rol
                // WithMany = un Rol puede tener muchos Usuarios
                // HasForeignKey = la FK en Usuario es IdRol
                // OnDelete.Restrict = evita que se borre un Rol si tiene Usuarios asociados
                entity.HasOne(e => e.Rol)
                      .WithMany(r => r.Usuario)
                      .HasForeignKey(e => e.IdRol)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación: un Usuario pertenece a un TipoDocumento
                // Similar a la anterior pero con la entidad TipoDocumento
                entity.HasOne(e => e.TipoDocumento).WithMany(t => t.Usuario).HasForeignKey(e => e.IdTipoDocumento).OnDelete(DeleteBehavior.Restrict);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("Usuarios");
            });
            // Modelo: Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                // Define la clave primaria de la entidad Rol
                entity.HasKey(e => e.IdRol);

                // Configura el nombre de la columna IdRol
                entity.Property(e => e.IdRol).HasColumnName("IdRol");

                // Configura la columna NombreRol, requerida y con longitud máxima 50
                entity.Property(e => e.NombreRol).IsRequired().HasMaxLength(50).HasColumnName("NombreRol");

                // Configura la columna DescripcionRol, requerida y con longitud máxima 250
                entity.Property(e => e.DescripcionRol).IsRequired().HasMaxLength(250).HasColumnName("DescripcionRol");

                // Relación: un Rol puede tener muchos Usuarios
                // HasMany = colección en Rol
                // WithOne = cada Usuario tiene un Rol
                // HasForeignKey = la FK en Usuario es IdRol
                entity.HasMany(r => r.Usuario).WithOne(u => u.Rol).HasForeignKey(u => u.IdRol);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("Roles");
            });
            // Modelo: TipoDocumento
            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                // Define la clave primaria de la entidad TipoDocumento
                entity.HasKey(e => e.IdTipoDocumento);

                // Configura el nombre de la columna IdTipoDocumento
                entity.Property(e => e.IdTipoDocumento).HasColumnName("IdTipoDocumento");

                // Configura la columna NombreDocumento, requerida y con longitud máxima 30
                entity.Property(e => e.NombreDocumento).IsRequired().HasMaxLength(30).HasColumnName("NombreDocumento");

                // Configura la columna Abreviatura, requerida y con longitud máxima 3
                entity.Property(e => e.Abreviatura).IsRequired().HasMaxLength(3).HasColumnName("Abreviatura");

                // Relación: un TipoDocumento puede tener muchos Usuarios
                // HasMany = colección en TipoDocumento
                // WithOne = cada Usuario tiene un TipoDocumento
                // HasForeignKey = la FK en Usuario es IdTipoDocumento
                entity.HasMany(t => t.Usuario)
                      .WithOne(u => u.TipoDocumento)
                      .HasForeignKey(u => u.IdTipoDocumento);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("TipoDocumentos");
            });

            // Modelo: SolicitudRecoleccion
            modelBuilder.Entity<SolicitudRecoleccion>(entity =>
            {
                // Define la clave primaria de la entidad SolicitudRecoleccion
                entity.HasKey(e => e.IdSolicitud);

                // Configura la propiedad IdSolicitud como nombre de columna en la base de datos
                entity.Property(e => e.IdSolicitud).HasColumnName("IdSolicitud");

                // Define que IdUsuario es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdUsuario).IsRequired().HasColumnName("IdUsuario");

                // Configura la columna DireccionRecoleccion, requerida y con longitud máxima 200
                entity.Property(e => e.DireccionRecoleccion).IsRequired().HasMaxLength(200).HasColumnName("DireccionRecoleccion");

                // Configura la columna EstadoActual, requerida y con longitud máxima 20
                entity.Property(e => e.EstadoActual).IsRequired().HasMaxLength(20).HasColumnName("EstadoActual");

                // Configura la columna FechaSolicitud con valor por defecto de la fecha actual
                entity.Property(e => e.FechaSolicitud).IsRequired().HasColumnName("FechaSolicitud").HasDefaultValueSql("GETDATE()");

                // Configura la columna TipoResiduos, requerida y con longitud máxima 100
                entity.Property(e => e.TipoResiduos).IsRequired().HasMaxLength(100).HasColumnName("TipoResiduos");

                // Configura la columna ObservacionesCiudadano, opcional (nullable) y con longitud máxima 500
                entity.Property(e => e.ObservacionesCiudadano).HasMaxLength(500).HasColumnName("ObservacionesCiudadano");

                // Relación: una SolicitudRecoleccion pertenece a un Usuario
                // HasOne = relación uno a uno o uno a muchos desde SolicitudRecoleccion hacia Usuario
                // WithMany = un Usuario puede tener muchas SolicitudesRecoleccion
                // HasForeignKey = la FK en SolicitudRecoleccion es IdUsuario
                // OnDelete.Restrict = evita que se borre un Usuario si tiene Solicitudes asociadas
                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("SolicitudRecoleccion");
            });

            // Modelo: GestionRecoleccion
            modelBuilder.Entity<GestionRecoleccion>(entity =>
            {
                // Define la clave primaria de la entidad GestionRecoleccion
                entity.HasKey(e => e.IdGestion);

                // Configura la propiedad IdGestion como nombre de columna en la base de datos
                entity.Property(e => e.IdGestion).HasColumnName("IdGestion");

                // Define que IdSolicitud es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdSolicitud).IsRequired().HasColumnName("IdSolicitud");

                // Define que IdGestor es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdGestor).IsRequired().HasColumnName("IdGestor");

                // Configura la columna Estado, requerida y con longitud máxima 20
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20).HasColumnName("Estado");

                // Configura la columna FechaCambioEstado, opcional (nullable)
                entity.Property(e => e.FechaCambioEstado).HasColumnName("FechaCambioEstado");

                // Configura la columna FechaProgramada, opcional (nullable)
                entity.Property(e => e.FechaProgramada).HasColumnName("FechaProgramada");

                // Configura la columna FechaRealizacion, opcional (nullable)
                entity.Property(e => e.FechaRealizacion).HasColumnName("FechaRealizacion");

                // Configura la columna ObservacionesGestor, opcional (nullable) y con longitud máxima 200
                entity.Property(e => e.ObservacionesGestor).HasMaxLength(200).HasColumnName("ObservacionesGestor");

                // Relación: una GestionRecoleccion pertenece a una SolicitudRecoleccion
                // HasOne = relación uno a uno o uno a muchos desde GestionRecoleccion hacia SolicitudRecoleccion
                // WithMany = una SolicitudRecoleccion puede tener muchas Gestiones
                // HasForeignKey = la FK en GestionRecoleccion es IdSolicitud
                // OnDelete.Restrict = evita que se borre una Solicitud si tiene Gestiones asociadas
                entity.HasOne(e => e.SolicitudRecoleccion)
                      .WithMany()
                      .HasForeignKey(e => e.IdSolicitud)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación: una GestionRecoleccion pertenece a un Usuario (Gestor)
                // HasOne = relación uno a uno o uno a muchos desde GestionRecoleccion hacia Usuario
                // WithMany = un Usuario puede tener muchas Gestiones asignadas
                // HasForeignKey = la FK en GestionRecoleccion es IdGestor
                // OnDelete.Restrict = evita que se borre un Usuario si tiene Gestiones asociadas
                entity.HasOne(e => e.Gestor)
                      .WithMany()
                      .HasForeignKey(e => e.IdGestor)
                      .OnDelete(DeleteBehavior.Restrict);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("GestionRecoleccion");
            });

            // Modelo: Notificacion
            modelBuilder.Entity<Notificacion>(entity =>
            {
                // Define la clave primaria de la entidad Notificacion
                entity.HasKey(e => e.IdNotificacion);

                // Configura la propiedad IdNotificacion como nombre de columna en la base de datos
                entity.Property(e => e.IdNotificacion).HasColumnName("IdNotificacion");

                // Define que IdUsuario es opcional (nullable)
                entity.Property(e => e.IdUsuario).HasColumnName("IdUsuario");

                // Define que IdSolicitud es opcional (nullable)
                entity.Property(e => e.IdSolicitud).HasColumnName("IdSolicitud");

                // Configura la columna Titulo, requerida y con longitud máxima 100
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100).HasColumnName("Titulo");

                // Configura la columna Mensaje, requerida y con longitud máxima 500
                entity.Property(e => e.Mensaje).IsRequired().HasMaxLength(500).HasColumnName("Mensaje");

                // Configura la columna Tipo, requerida y con longitud máxima 50
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50).HasColumnName("Tipo");

                // Configura la columna FechaCreacion con valor por defecto de la fecha actual
                entity.Property(e => e.FechaCreacion).IsRequired().HasColumnName("FechaCreacion").HasDefaultValueSql("GETDATE()");

                // Configura la columna Leida, requerida con valor por defecto false
                entity.Property(e => e.Leida).IsRequired().HasColumnName("Leida").HasDefaultValue(false);

                // Relación: una Notificacion puede pertenecer a un Usuario (opcional)
                // HasOne = relación uno a uno o uno a muchos desde Notificacion hacia Usuario
                // WithMany = un Usuario puede tener muchas Notificaciones
                // HasForeignKey = la FK en Notificacion es IdUsuario
                // IsRequired(false) = la relación es opcional
                // OnDelete.Cascade = si se borra un Usuario, se borran sus Notificaciones
                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.IdUsuario)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación: una Notificacion puede pertenecer a una SolicitudRecoleccion (opcional)
                // HasOne = relación uno a uno o uno a muchos desde Notificacion hacia SolicitudRecoleccion
                // WithMany = una SolicitudRecoleccion puede tener muchas Notificaciones
                // HasForeignKey = la FK en Notificacion es IdSolicitud
                // IsRequired(false) = la relación es opcional
                // OnDelete.Cascade = si se borra una Solicitud, se borran sus Notificaciones
                entity.HasOne(e => e.SolicitudRecoleccion)
                      .WithMany()
                      .HasForeignKey(e => e.IdSolicitud)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Cascade);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("Notificaciones");
            });

            // Modelo: Historial
            modelBuilder.Entity<Historial>(entity =>
            {
                // Define la clave primaria de la entidad Historial
                entity.HasKey(e => e.IdHistorial);

                // Configura la propiedad IdHistorial como nombre de columna en la base de datos
                entity.Property(e => e.IdHistorial).HasColumnName("IdHistorial");

                // Define que IdSolicitud es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdSolicitud).IsRequired().HasColumnName("IdSolicitud");

                // Define que IdUsuario es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdUsuario).IsRequired().HasColumnName("IdUsuario");

                // Configura la columna EstadoAnterior, opcional (nullable) y con longitud máxima 20
                entity.Property(e => e.EstadoAnterior).HasMaxLength(20).HasColumnName("EstadoAnterior");

                // Configura la columna EstadoNuevo, requerida y con longitud máxima 20
                entity.Property(e => e.EstadoNuevo).IsRequired().HasMaxLength(20).HasColumnName("EstadoNuevo");

                // Configura la columna FechaCambio con valor por defecto de la fecha actual
                entity.Property(e => e.FechaCambio).IsRequired().HasColumnName("FechaCambio").HasDefaultValueSql("GETDATE()");

                // Configura la columna Comentario, opcional (nullable) y con longitud máxima 500
                entity.Property(e => e.Comentario).HasMaxLength(500).HasColumnName("Comentario");

                // Relación: un Historial pertenece a una SolicitudRecoleccion
                // HasOne = relación uno a uno o uno a muchos desde Historial hacia SolicitudRecoleccion
                // WithMany = una SolicitudRecoleccion puede tener muchos Historiales
                // HasForeignKey = la FK en Historial es IdSolicitud
                // OnDelete.Restrict = evita que se borre una Solicitud si tiene Historiales asociados
                entity.HasOne(e => e.SolicitudRecoleccion)
                      .WithMany()
                      .HasForeignKey(e => e.IdSolicitud)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación: un Historial pertenece a un Usuario
                // HasOne = relación uno a uno o uno a muchos desde Historial hacia Usuario
                // WithMany = un Usuario puede tener muchos Historiales
                // HasForeignKey = la FK en Historial es IdUsuario
                // OnDelete.Restrict = evita que se borre un Usuario si tiene Historiales asociados
                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("Historial");
            });

            // Modelo: HistorialChat
            modelBuilder.Entity<HistorialChat>(entity =>
            {
                // Define la clave primaria de la entidad HistorialChat
                entity.HasKey(e => e.IdHistorialChat);

                // Configura la propiedad IdHistorialChat como nombre de columna en la base de datos
                entity.Property(e => e.IdHistorialChat).HasColumnName("IdHistorialChat");

                // Define que IdSolicitud es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdSolicitud).IsRequired().HasColumnName("IdSolicitud");

                // Define que IdEmisor es obligatorio (no puede ser nulo)
                entity.Property(e => e.IdEmisor).IsRequired().HasColumnName("IdEmisor");

                // Configura la columna Mensaje, requerida y con longitud máxima 1000
                entity.Property(e => e.Mensaje).IsRequired().HasMaxLength(1000).HasColumnName("Mensaje");

                // Configura la columna FechaEnvio con valor por defecto de la fecha actual
                entity.Property(e => e.FechaEnvio).IsRequired().HasColumnName("FechaEnvio").HasDefaultValueSql("GETDATE()");

                // Configura la columna Leido, requerida con valor por defecto false
                entity.Property(e => e.Leido).IsRequired().HasColumnName("Leido").HasDefaultValue(false);

                // Relación: un HistorialChat pertenece a una SolicitudRecoleccion
                entity.HasOne(e => e.SolicitudRecoleccion)
                      .WithMany()
                      .HasForeignKey(e => e.IdSolicitud)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación: un HistorialChat pertenece a un Usuario (Emisor)
                entity.HasOne(e => e.Emisor)
                      .WithMany()
                      .HasForeignKey(e => e.IdEmisor)
                      .OnDelete(DeleteBehavior.Restrict);

                // Asigna el nombre de la tabla física en la base de datos
                entity.ToTable("HistorialChat");
            });

            // Llama al método base por si se requiere configuración adicional
            base.OnModelCreating(modelBuilder);

        }
    }
}
