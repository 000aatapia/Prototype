using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IConfiguracionInstancias
    {
        EspecificacionInstancia ObtenerEspecificaciones(ProveedorNube proveedor, string tipoInstancia);
        Dictionary<string, object> ObtenerInstanciasDisponibles(ProveedorNube proveedor);
    }

    public record EspecificacionInstancia(int VCpus, int MemoryGB);
}
