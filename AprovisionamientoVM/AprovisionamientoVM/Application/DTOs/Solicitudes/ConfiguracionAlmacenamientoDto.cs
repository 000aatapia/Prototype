using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Solicitudes
{
    public class ConfiguracionAlmacenamientoDto
    {
        public string Region { get; set; } = string.Empty;
        public int? Iops { get; set; }
    }
}
