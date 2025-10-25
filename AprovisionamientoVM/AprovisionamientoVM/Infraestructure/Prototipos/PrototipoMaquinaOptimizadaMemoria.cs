using Domain.Entidades;
using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Prototipos
{
    public class PrototipoMaquinaOptimizadaMemoria : MaquinaVirtual
    {
        public PrototipoMaquinaOptimizadaMemoria(ProveedorNube proveedor, string tipoInstancia, int vcpus, int memoryGB, string regionDefecto)
        {
            Provider = proveedor;
            TipoMaquina = TipoMaquina.OptimizadaMemoria;
            TipoInstancia = tipoInstancia;
            VCpus = vcpus;
            MemoryGB = memoryGB;
            MemoryOptimization = true;
            DiskOptimization = false;
            KeyPairName = "default-key";

            Red = new Red
            {
                Proveedor = proveedor,
                Region = regionDefecto,
                PublicIP = false
            };

            Almacenamiento = new Almacenamiento
            {
                Proveedor = proveedor,
                Region = regionDefecto,
                Iops = 5000
            };
        }
    }
}
