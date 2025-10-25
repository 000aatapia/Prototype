using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Respuestas
{
    public class RespuestaMaquinaVirtualDto
    {
        public string Id { get; set; } = string.Empty;
        public ProveedorNube Proveedor { get; set; }
        public TipoMaquina TipoMaquina { get; set; }
        public string TipoInstancia { get; set; } = string.Empty;
        public int VCpus { get; set; }
        public int MemoryGB { get; set; }
        public bool MemoryOptimization { get; set; }
        public bool DiskOptimization { get; set; }
        public string? KeyPairName { get; set; }

        public DetalleRedDto Red { get; set; } = new DetalleRedDto();
        public DetalleAlmacenamientoDto Almacenamiento { get; set; } = new DetalleAlmacenamientoDto();
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }

    public class DetalleRedDto
    {
        public string Region { get; set; } = string.Empty;
        public List<string>? FirewallRules { get; set; }
        public bool? PublicIP { get; set; }
    }

    public class DetalleAlmacenamientoDto
    {
        public string Region { get; set; } = string.Empty;
        public int? Iops { get; set; }
    }
}
