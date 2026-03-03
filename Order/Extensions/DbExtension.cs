using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System.Runtime.CompilerServices;

namespace Order.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TConext>(this IHost host, Action<TConext, IServiceProvider> seeder) where TConext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TConext>>();
                var context = services.GetService<TConext>();


                try
                {
                    logger.LogInformation($"Started Db Migration : {typeof(TConext).Name}");

                    //retry strategy for Db Migration
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, span, count) =>
                            {
                                logger.LogError($"Retrying because of {exception} {span}");
                            }
                        );

                    retry.Execute(() => CallSeeder(seeder, context, services));
                    logger.LogInformation($"Migration Completed: {typeof(TConext).Name}");


                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating db: {typeof(TConext).Name}");
                }
            }
            return host;

        }

        private static void CallSeeder<TConext>(Action<TConext, IServiceProvider> seeder, TConext? context, IServiceProvider services) where TConext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
