using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Solicitudes
{
    public class ConfiguracionRedDto
    {
        public string Region { get; set; } = string.Empty;
        public List<string>? FirewallRules { get; set; }
        public bool? PublicIP { get; set; }
    }
}
