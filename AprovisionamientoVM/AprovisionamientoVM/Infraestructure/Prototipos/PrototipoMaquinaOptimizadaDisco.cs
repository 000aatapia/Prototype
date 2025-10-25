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
        public PrototipoMaquinaOptimizadaDisco(ProveedorNube proveedor, string tipoInstancia, int vcpus, int memoryGB)
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
                Region = ObtenerRegionPorDefecto(proveedor),
                PublicIP = false
            };
            Almacenamiento = new Almacenamiento
            {
                Proveedor = proveedor,
                Region = ObtenerRegionPorDefecto(proveedor),
                Iops = 10000
            };
        }

        private string ObtenerRegionPorDefecto(ProveedorNube proveedor)
        {
            return proveedor switch
            {
                ProveedorNube.AWS => "us-east-1",
                ProveedorNube.Azure => "eastus",
                ProveedorNube.GCP => "us-central1",
                ProveedorNube.OnPremise => "datacenter-1",
                _ => "default"
            };
        }
    }
}
