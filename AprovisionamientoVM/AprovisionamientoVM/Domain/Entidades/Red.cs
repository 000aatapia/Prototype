using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Red
    {
        public string Region { get; set; } = string.Empty;
        public List<string>? FirewallRules { get; set; }
        public bool? PublicIP { get; set; }
        public ProveedorNube Proveedor { get; set; }

        public Red Clonar()
        {
            return new Red
            {
                Region = this.Region,
                FirewallRules = this.FirewallRules?.ToList(),
                PublicIP = this.PublicIP,
                Proveedor = this.Proveedor
            };
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Region))
            {
                throw new Excepciones.ValidacionRecursoExcepcion("La región es obligatoria para la red");
            }
        }
    }
}
