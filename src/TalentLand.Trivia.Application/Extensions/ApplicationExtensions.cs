using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TalentLand.Trivia.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();            
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assembly);
            return services;
        }
    }
}
