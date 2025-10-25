using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositorioMaquinasVirtuales
    {
        Task Agregar(MaquinaVirtual vm);
        Task<List<MaquinaVirtual>> ObtenerTodas();
        Task<MaquinaVirtual?> ObtenerPorId(string id);
    }
}
