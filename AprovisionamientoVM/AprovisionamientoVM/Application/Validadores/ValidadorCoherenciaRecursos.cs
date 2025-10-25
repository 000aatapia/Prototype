using Domain.Entidades;
using Domain.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validadores
{
    public class ValidadorCoherenciaRecursos
    {
        public void ValidarMaquinaVirtual(MaquinaVirtual vm)
        {
            vm.Validar();
            vm.ValidarCoherenciaProveedor();
            vm.ValidarCoherenciaRegion();
        }
    }
}
