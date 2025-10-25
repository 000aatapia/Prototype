using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Almacenamiento
    {
        public string Region { get; set; } = string.Empty;
        public int? Iops { get; set; }
        public ProveedorNube Proveedor { get; set; }


        public Almacenamiento Clonar()
        {
            return new Almacenamiento
            {
                Region = this.Region,
                Iops = this.Iops,
                Proveedor = this.Proveedor
            };
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Region))
            {
                throw new Excepciones.ValidacionRecursoExcepcion("La región es obligatoria para el almacenamiento");
            }
        }
    }
}
