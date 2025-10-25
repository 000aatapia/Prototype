using Application.DTOs.Respuestas;
using Application.DTOs.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servicios.Interfaces
{
    public interface IServicioAprovisionamiento
    {
        Task<RespuestaMaquinaVirtualDto> CrearMaquinaVirtual(SolicitudCrearMaquinaVirtualDto solicitud);
        Task<List<RespuestaMaquinaVirtualDto>> ObtenerTodasLasMaquinas();
        Task<RespuestaMaquinaVirtualDto?> ObtenerMaquinaPorId(string id);
        Task<Dictionary<string, object>> ObtenerTiposInstanciaDisponibles(string proveedor);
    }
}
