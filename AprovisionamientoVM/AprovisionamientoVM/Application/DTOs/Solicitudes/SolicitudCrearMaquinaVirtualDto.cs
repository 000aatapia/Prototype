using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Solicitudes
{
    public class SolicitudCrearMaquinaVirtualDto
    {
        public ProveedorNube Proveedor { get; set; }
        public TipoMaquina TipoMaquina { get; set; }
        public string? TipoInstancia { get; set; }
        public string? KeyPairName { get; set; }

        public ConfiguracionRedDto Red { get; set; } = new ConfiguracionRedDto();
        public ConfiguracionAlmacenamientoDto Almacenamiento { get; set; } = new ConfiguracionAlmacenamientoDto();
    }
}
