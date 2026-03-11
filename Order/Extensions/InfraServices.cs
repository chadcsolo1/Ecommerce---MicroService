using Microsoft.EntityFrameworkCore;
using Order.Data;
using Order.Repositories;

namespace Order.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultSQLConnection"),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            });
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
