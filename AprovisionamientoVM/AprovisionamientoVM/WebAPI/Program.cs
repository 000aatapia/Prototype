using Application.Interfaces;
using Application.Mapeadores;
using Application.Servicios.Implementaciones;
using Application.Servicios.Interfaces;
using Application.Validadores;
using Infraestructure.Configuraciones;
using Infraestructure.Prototipos;
using Infraestructure.Repositorios;
using Microsoft.OpenApi.Models;
using WebAPI.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Aprovisionamiento Multi-Cloud",
        Version = "v1",
        Description = "API REST para aprovisionamiento de máquinas virtuales usando el patrón Prototype"
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddSingleton<IConfiguracionInstancias, ConfiguracionInstanciasPorProveedor>();
builder.Services.AddSingleton<IRegistroPrototipos, RegistroPrototipos>();
builder.Services.AddSingleton<IRepositorioMaquinasVirtuales, RepositorioMaquinasVirtualesEnMemoria>();

builder.Services.AddScoped<ValidadorCoherenciaRecursos>();
builder.Services.AddScoped<MapeadorRecursos>();
builder.Services.AddScoped<IServicioAprovisionamiento, ServicioAprovisionamiento>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Aprovisionamiento V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<ManejadorExcepcionesGlobal>();
app.UseHttpsRedirection();
app.UseCors("PermitirTodo");
app.UseAuthorization();
app.MapControllers();

app.Logger.LogInformation("API de Aprovisionamiento Multi-Cloud iniciada correctamente");

app.Run();