using Microsoft.EntityFrameworkCore;
using RecyRoute.Context;
using RecyRoute.Repositories;
using RecyRoute.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace RecyRoute
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "";
            connectionString = configuration["ConnectionStrings:SQLConnectionStrings"];

            services.AddDbContext<RecyRouteContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();
            services.AddScoped<IRolRepository, RolRepository>();

            return services;

        }
    }
}
