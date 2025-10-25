using Domain.Entidades;
using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRegistroPrototipos
    {
        MaquinaVirtual ObtenerPrototipo(ProveedorNube proveedor, TipoMaquina tipoMaquina);
    }
}
