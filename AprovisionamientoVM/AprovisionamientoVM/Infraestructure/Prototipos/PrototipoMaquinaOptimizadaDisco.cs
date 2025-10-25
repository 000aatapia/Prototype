using Domain.Entidades;
using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Prototipos
{
    public class PrototipoMaquinaOptimizadaDisco : MaquinaVirtual
    {
        public PrototipoMaquinaOptimizadaDisco(ProveedorNube proveedor, string tipoInstancia, int vcpus, int memoryGB, string regionDefecto)
        {
            Provider = proveedor;
            TipoMaquina = TipoMaquina.OptimizadaDisco;
            TipoInstancia = tipoInstancia;
            VCpus = vcpus;
            MemoryGB = memoryGB;
            MemoryOptimization = false;
            DiskOptimization = true;
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
                Iops = 10000
            };
        }
    }
}
