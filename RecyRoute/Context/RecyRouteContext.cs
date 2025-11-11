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

            // Llama al método base por si se requiere configuración adicional
            base.OnModelCreating(modelBuilder);
        }
    }
}
