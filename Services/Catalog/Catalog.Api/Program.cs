using Catalog.Application.Commands;
using Catalog.Application.Handlers;
using Catalog.Infraestructure.Data.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = builder.Configuration;


builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddHealthChecks()
                .AddMongoDb(Configuration["DatabaseSettings:ConnectionString"], "Catalog MongoDb Health Check",
                    HealthStatus.Degraded);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo(){ Title = "Catalog .API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CreateProductHandler).Assembly));

builder.Services.AddInjectionInfrestructure(Configuration);

var app = builder.Build();
app.MapGet("/", () => "Hello World");
//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API"));
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.Run();
