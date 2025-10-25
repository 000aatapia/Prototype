using Application.Interfaces;
using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configuraciones
{
    public class ConfiguracionInstanciasPorProveedor : IConfiguracionInstancias
    {
        private readonly Dictionary<ProveedorNube, Dictionary<string, (int VCpus, int MemoryGB, TipoMaquina Tipo)>> _catalogo;

        public ConfiguracionInstanciasPorProveedor()
        {
            _catalogo = new Dictionary<ProveedorNube, Dictionary<string, (int, int, TipoMaquina)>>
        {
            {
                ProveedorNube.AWS, new Dictionary<string, (int, int, TipoMaquina)>
                {
                    { "t3.medium", (2, 4, TipoMaquina.Standard) },
                    { "m5.large", (2, 8, TipoMaquina.Standard) },
                    { "m5.xlarge", (4, 16, TipoMaquina.Standard) },
                    { "r5.large", (2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "r5.xlarge", (4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "r5.2xlarge", (8, 64, TipoMaquina.OptimizadaMemoria) },
                    { "c5.large", (2, 4, TipoMaquina.OptimizadaDisco) },
                    { "c5.xlarge", (4, 8, TipoMaquina.OptimizadaDisco) },
                    { "c5.2xlarge", (8, 16, TipoMaquina.OptimizadaDisco) }
                }
            },
            {
                ProveedorNube.Azure, new Dictionary<string, (int, int, TipoMaquina)>
                {
                    { "D2s_v3", (2, 8, TipoMaquina.Standard) },
                    { "D4s_v3", (4, 16, TipoMaquina.Standard) },
                    { "D8s_v3", (8, 32, TipoMaquina.Standard) },
                    { "E2s_v3", (2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "E4s_v3", (4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "E8s_v3", (8, 64, TipoMaquina.OptimizadaMemoria) },
                    { "F2s_v2", (2, 4, TipoMaquina.OptimizadaDisco) },
                    { "F4s_v2", (4, 8, TipoMaquina.OptimizadaDisco) },
                    { "F8s_v2", (8, 16, TipoMaquina.OptimizadaDisco) }
                }
            },
            {
                ProveedorNube.GCP, new Dictionary<string, (int, int, TipoMaquina)>
                {
                    { "e2-standard-2", (2, 8, TipoMaquina.Standard) },
                    { "e2-standard-4", (4, 16, TipoMaquina.Standard) },
                    { "e2-standard-8", (8, 32, TipoMaquina.Standard) },
                    { "n2-highmem-2", (2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "n2-highmem-4", (4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "n2-highmem-8", (8, 64, TipoMaquina.OptimizadaMemoria) },
                    { "n2-highcpu-2", (2, 2, TipoMaquina.OptimizadaDisco) },
                    { "n2-highcpu-4", (4, 4, TipoMaquina.OptimizadaDisco) },
                    { "n2-highcpu-8", (8, 8, TipoMaquina.OptimizadaDisco) }
                }
            },
            {
                ProveedorNube.OnPremise, new Dictionary<string, (int, int, TipoMaquina)>
                {
                    { "onprem-std1", (2, 4, TipoMaquina.Standard) },
                    { "onprem-std2", (4, 8, TipoMaquina.Standard) },
                    { "onprem-std3", (8, 16, TipoMaquina.Standard) },
                    { "onprem-mem1", (2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "onprem-mem2", (4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "onprem-mem3", (8, 64, TipoMaquina.OptimizadaMemoria) },
                    { "onprem-cpu1", (2, 2, TipoMaquina.OptimizadaDisco) },
                    { "onprem-cpu2", (4, 4, TipoMaquina.OptimizadaDisco) },
                    { "onprem-cpu3", (8, 8, TipoMaquina.OptimizadaDisco) }
                }
            }
        };
        }

        public EspecificacionInstancia ObtenerEspecificaciones(ProveedorNube proveedor, string tipoInstancia)
        {
            if (!_catalogo.ContainsKey(proveedor))
                throw new ArgumentException($"Proveedor {proveedor} no soportado");

            if (!_catalogo[proveedor].ContainsKey(tipoInstancia))
                throw new ArgumentException($"Tipo de instancia {tipoInstancia} no existe para {proveedor}");

            var specs = _catalogo[proveedor][tipoInstancia];
            return new EspecificacionInstancia(specs.VCpus, specs.MemoryGB);
        }

        public Dictionary<string, object> ObtenerInstanciasDisponibles(ProveedorNube proveedor)
        {
            if (!_catalogo.ContainsKey(proveedor))
                throw new ArgumentException($"Proveedor {proveedor} no soportado");

            var resultado = new Dictionary<string, object>();

            foreach (var categoria in _catalogo[proveedor].GroupBy(x => x.Value.Tipo))
            {
                var instancias = categoria.Select(x => new
                {
                    Nombre = x.Key,
                    VCpus = x.Value.VCpus,
                    MemoryGB = x.Value.MemoryGB
                }).ToList();

                resultado[categoria.Key.ToString()] = instancias;
            }

            return resultado;
        }
    }

}