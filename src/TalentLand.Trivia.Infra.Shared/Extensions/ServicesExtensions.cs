using Microsoft.Extensions.DependencyInjection;
using TalentLand.Trivia.Application.Interfaces.Services;

namespace TalentLand.Trivia.Infra.Shared.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<ITriviaFunctionService, TriviaFunctionService>();
            return services;
        }
    }
}
