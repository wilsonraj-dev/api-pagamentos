using API.Pagamentos.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Pagamentos.IoC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            return services;
        }
    }
}
