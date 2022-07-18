using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using TalentLand.Trivia.Application.Extensions;
using TalentLand.Trivia.Infra.Persistence;
using TalentLand.Trivia.Infra.Persistence.Extensions;

[assembly: FunctionsStartup(typeof(TalentLand.Trivia.Function.Startup))]
namespace TalentLand.Trivia.Function
{
    public class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton(typeof(IConfiguration), _configuration);

            builder.Services.AddApplicationConfiguration();

            //Context and Repositories
            builder.Services.AddRepositories();
            builder.Services.AddContext<ApplicationDbContext>(_configuration);            
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            _configuration = builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables().Build();
        }
    }

}
