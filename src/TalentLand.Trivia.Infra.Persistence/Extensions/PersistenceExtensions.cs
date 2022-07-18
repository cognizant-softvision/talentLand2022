using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalentLand.Trivia.Infra.Persistence.Options;

namespace TalentLand.Trivia.Infra.Persistence.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddContext<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext: BaseDbContext
        {
            var section = configuration.GetSection(ConnectionOptions.ConnectionParameters);
            var connectionOptions = section.Get<ConnectionOptions>();
            services.AddScoped(typeof(BaseDbContext), typeof(TContext))
                .Configure<ConnectionOptions>(options => section.Bind(options));

            if (connectionOptions.UseInMemory)
            {
                services.AddDbContext<TContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<TContext>(options =>
                    options.UseSqlServer(connectionOptions.ConnectionString));
            }

            return services;
        }
    }
}
