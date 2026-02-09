using BuildingBlocks.Application.Behaviors;
using FeedEngine.DDD.API.Extensions;
using FeedEngine.DDD.API.Middleware;
using FluentValidation;
using Identity.Application;
using Identity.Infrastructure;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration) // read from appsettings.json
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});
// Add services to the container.
builder.Services
    .AddIdentityInfrastructure(builder.Configuration)
    .AddIdentityApplication();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
