using Application.DTOs.Respuestas;
using Application.DTOs.Solicitudes;
using Application.Servicios.Interfaces;
using Domain.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AprovisionamientoController : ControllerBase
    {
        private readonly IServicioAprovisionamiento _servicioAprovisionamiento;
        private readonly ILogger<AprovisionamientoController> _logger;

        public AprovisionamientoController(
            IServicioAprovisionamiento servicioAprovisionamiento,
            ILogger<AprovisionamientoController> logger)
        {
            _servicioAprovisionamiento = servicioAprovisionamiento;
            _logger = logger;
        }

        [HttpPost("maquinas-virtuales")]
        [ProducesResponseType(typeof(RespuestaMaquinaVirtualDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RespuestaMaquinaVirtualDto>> CrearMaquinaVirtual(
            [FromBody] SolicitudCrearMaquinaVirtualDto solicitud)
        {
            try
            {
                _logger.LogInformation(
                    "Creando máquina virtual - Proveedor: {Proveedor}, Tipo: {TipoMaquina}",
                    solicitud.Proveedor,
                    solicitud.TipoMaquina);

                var resultado = await _servicioAprovisionamiento.CrearMaquinaVirtual(solicitud);

                _logger.LogInformation("Máquina virtual creada exitosamente - ID: {Id}", resultado.Id);

                return CreatedAtAction(
                    nameof(ObtenerMaquinaPorId),
                    new { id = resultado.Id },
                    resultado);
            }
            catch (ValidacionRecursoExcepcion ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear máquina virtual");
                return BadRequest(new { Error = "Error de validación", Detalle = ex.Message });
            }
            catch (IncoherenciaProveedorExcepcion ex)
            {
                _logger.LogWarning(ex, "Error de coherencia de recursos");
                return BadRequest(new { Error = "Error de coherencia", Detalle = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear máquina virtual");
                return StatusCode(500, new { Error = "Error interno del servidor" });
            }
        }

        [HttpGet("maquinas-virtuales")]
        [ProducesResponseType(typeof(List<RespuestaMaquinaVirtualDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RespuestaMaquinaVirtualDto>>> ObtenerTodasLasMaquinas()
        {
            _logger.LogInformation("Obteniendo todas las máquinas virtuales");
            var maquinas = await _servicioAprovisionamiento.ObtenerTodasLasMaquinas();
            return Ok(maquinas);
        }

        [HttpGet("maquinas-virtuales/{id}")]
        [ProducesResponseType(typeof(RespuestaMaquinaVirtualDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RespuestaMaquinaVirtualDto>> ObtenerMaquinaPorId(string id)
        {
            _logger.LogInformation("Obteniendo máquina virtual por ID: {Id}", id);

            var maquina = await _servicioAprovisionamiento.ObtenerMaquinaPorId(id);

            if (maquina == null)
            {
                _logger.LogWarning("Máquina virtual no encontrada - ID: {Id}", id);
                return NotFound(new { Error = "Máquina virtual no encontrada", Id = id });
            }

            return Ok(maquina);
        }

        [HttpGet("proveedores/{proveedor}/instancias")]
        [ProducesResponseType(typeof(Dictionary<string, object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dictionary<string, object>>> ObtenerInstanciasDisponibles(string proveedor)
        {
            try
            {
                _logger.LogInformation("Obteniendo instancias disponibles para proveedor: {Proveedor}", proveedor);

                var instancias = await _servicioAprovisionamiento.ObtenerTiposInstanciaDisponibles(proveedor);

                return Ok(instancias);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Proveedor no válido: {Proveedor}", proveedor);
                return BadRequest(new { Error = "Proveedor no válido", Detalle = ex.Message });
            }
        }

        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Health()
        {
            return Ok(new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Service = "API de Aprovisionamiento Multi-Cloud"
            });
        }
    }
}
