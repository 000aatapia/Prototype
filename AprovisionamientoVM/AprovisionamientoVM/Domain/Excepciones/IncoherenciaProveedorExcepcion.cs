using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excepciones
{
    public class IncoherenciaProveedorExcepcion : Exception
    {
        public IncoherenciaProveedorExcepcion(string mensaje) : base(mensaje)
        {
        }

        public IncoherenciaProveedorExcepcion(string mensaje, Exception innerException)
            : base(mensaje, innerException)
        {
        }
    }
}
