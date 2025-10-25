using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excepciones
{
    public class ValidacionRecursoExcepcion : Exception
    {
        public ValidacionRecursoExcepcion(string mensaje) : base(mensaje)
        {
        }

        public ValidacionRecursoExcepcion(string mensaje, Exception innerException)
            : base(mensaje, innerException)
        {
        }
    }
}
