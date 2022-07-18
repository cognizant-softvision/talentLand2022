using Microsoft.Extensions.DependencyInjection;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Infra.Persistence.Repositories;

namespace TalentLand.Trivia.Infra.Persistence.Extensions
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQARepository, QARepository>();
            return services;
        }
    }
}
