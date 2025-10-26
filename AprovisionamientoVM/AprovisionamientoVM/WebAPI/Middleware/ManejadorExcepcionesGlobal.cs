using Domain.Excepciones;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middleware
{
    public class ManejadorExcepcionesGlobal
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorExcepcionesGlobal> _logger;

        public ManejadorExcepcionesGlobal(
            RequestDelegate next,
            ILogger<ManejadorExcepcionesGlobal> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado en la aplicación");
                await ManejarExcepcion(context, ex);
            }
        }

        private async Task ManejarExcepcion(HttpContext context, Exception excepcion)
        {
            context.Response.ContentType = "application/json";

            var respuesta = excepcion switch
            {
                ValidacionRecursoExcepcion => new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Mensaje = "Error de validación",
                    Detalle = excepcion.Message
                },
                IncoherenciaProveedorExcepcion => new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Mensaje = "Error de coherencia de recursos",
                    Detalle = excepcion.Message
                },
                KeyNotFoundException => new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Mensaje = "Recurso no encontrado",
                    Detalle = excepcion.Message
                },
                ArgumentException => new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Mensaje = "Argumento inválido",
                    Detalle = excepcion.Message
                },
                _ => new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Mensaje = "Error interno del servidor",
                    Detalle = "Ocurrió un error inesperado"
                }
            };

            context.Response.StatusCode = respuesta.StatusCode;

            var jsonRespuesta = JsonSerializer.Serialize(respuesta, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonRespuesta);
        }
    }
}
