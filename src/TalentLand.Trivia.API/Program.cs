using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using System.Text.Json.Serialization;
using TalentLand.Trivia.Application.Extensions;
using TalentLand.Trivia.Application.Features.User.GetUsers;
using TalentLand.Trivia.Infra.Persistence;
using TalentLand.Trivia.Infra.Persistence.Extensions;
using TalentLand.Trivia.Infra.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}).AddMvcOptions(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.RespectBrowserAcceptHeader = true;
}).AddFluentValidation(s =>
{
    s.RegisterValidatorsFromAssemblyContaining(typeof(GetUsersValidator));
    s.AutomaticValidationEnabled = true;
});

//Configuration
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddApplicationConfiguration();

builder.Services.AddHttpContextAccessor();

//Context and Repositories
builder.Services.AddRepositories();
builder.Services.AddContext<ApplicationDbContext>(builder.Configuration);

//Add Shared Services 
builder.Services.AddSharedServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/build";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    spa.Options.StartupTimeout = new TimeSpan(0, 20, 0);

    if (app.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});

app.Run();
