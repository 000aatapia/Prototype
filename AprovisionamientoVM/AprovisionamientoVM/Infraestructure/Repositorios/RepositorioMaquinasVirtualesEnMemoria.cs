using Application.Interfaces;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorios
{
    public class RepositorioMaquinasVirtualesEnMemoria : IRepositorioMaquinasVirtuales
    {
        private readonly List<MaquinaVirtual> _maquinas;
        private readonly object _lock = new object();

        public RepositorioMaquinasVirtualesEnMemoria()
        {
            _maquinas = new List<MaquinaVirtual>();
        }

        public Task Agregar(MaquinaVirtual vm)
        {
            lock (_lock)
            {
                _maquinas.Add(vm);
            }
            return Task.CompletedTask;
        }

        public Task<List<MaquinaVirtual>> ObtenerTodas()
        {
            lock (_lock)
            {
                return Task.FromResult(new List<MaquinaVirtual>(_maquinas));
            }
        }

        public Task<MaquinaVirtual?> ObtenerPorId(string id)
        {
            lock (_lock)
            {
                var vm = _maquinas.FirstOrDefault(v => v.Id == id);
                return Task.FromResult(vm);
            }
        }
    }
}
