using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configuraciones
{
    public class ConfiguracionInstanciasPorProveedor
    {
        private readonly Dictionary<ProveedorNube, Dictionary<string, EspecificacionesInstancia>> _catalogo;

        public ConfiguracionInstanciasPorProveedor()
        {
            _catalogo = new Dictionary<ProveedorNube, Dictionary<string, EspecificacionesInstancia>>
        {
            {
                ProveedorNube.AWS, new Dictionary<string, EspecificacionesInstancia>
                {
                    { "t3.medium", new EspecificacionesInstancia(2, 4, TipoMaquina.Standard) },
                    { "m5.large", new EspecificacionesInstancia(2, 8, TipoMaquina.Standard) },
                    { "m5.xlarge", new EspecificacionesInstancia(4, 16, TipoMaquina.Standard) },
                    
                    { "r5.large", new EspecificacionesInstancia(2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "r5.xlarge", new EspecificacionesInstancia(4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "r5.2xlarge", new EspecificacionesInstancia(8, 64, TipoMaquina.OptimizadaMemoria) },
                    
                    { "c5.large", new EspecificacionesInstancia(2, 4, TipoMaquina.OptimizadaDisco) },
                    { "c5.xlarge", new EspecificacionesInstancia(4, 8, TipoMaquina.OptimizadaDisco) },
                    { "c5.2xlarge", new EspecificacionesInstancia(8, 16, TipoMaquina.OptimizadaDisco) }
                }
            },
            
            {
                ProveedorNube.Azure, new Dictionary<string, EspecificacionesInstancia>
                {
                    { "D2s_v3", new EspecificacionesInstancia(2, 8, TipoMaquina.Standard) },
                    { "D4s_v3", new EspecificacionesInstancia(4, 16, TipoMaquina.Standard) },
                    { "D8s_v3", new EspecificacionesInstancia(8, 32, TipoMaquina.Standard) },
                    
                    { "E2s_v3", new EspecificacionesInstancia(2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "E4s_v3", new EspecificacionesInstancia(4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "E8s_v3", new EspecificacionesInstancia(8, 64, TipoMaquina.OptimizadaMemoria) },
                    
                    { "F2s_v2", new EspecificacionesInstancia(2, 4, TipoMaquina.OptimizadaDisco) },
                    { "F4s_v2", new EspecificacionesInstancia(4, 8, TipoMaquina.OptimizadaDisco) },
                    { "F8s_v2", new EspecificacionesInstancia(8, 16, TipoMaquina.OptimizadaDisco) }
                }
            },
            
            {
                ProveedorNube.GCP, new Dictionary<string, EspecificacionesInstancia>
                {
                    { "e2-standard-2", new EspecificacionesInstancia(2, 8, TipoMaquina.Standard) },
                    { "e2-standard-4", new EspecificacionesInstancia(4, 16, TipoMaquina.Standard) },
                    { "e2-standard-8", new EspecificacionesInstancia(8, 32, TipoMaquina.Standard) },
                    
                    { "n2-highmem-2", new EspecificacionesInstancia(2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "n2-highmem-4", new EspecificacionesInstancia(4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "n2-highmem-8", new EspecificacionesInstancia(8, 64, TipoMaquina.OptimizadaMemoria) },
                    
                    { "n2-highcpu-2", new EspecificacionesInstancia(2, 2, TipoMaquina.OptimizadaDisco) },
                    { "n2-highcpu-4", new EspecificacionesInstancia(4, 4, TipoMaquina.OptimizadaDisco) },
                    { "n2-highcpu-8", new EspecificacionesInstancia(8, 8, TipoMaquina.OptimizadaDisco) }
                }
            },
            
            {
                ProveedorNube.OnPremise, new Dictionary<string, EspecificacionesInstancia>
                {
                    { "onprem-std1", new EspecificacionesInstancia(2, 4, TipoMaquina.Standard) },
                    { "onprem-std2", new EspecificacionesInstancia(4, 8, TipoMaquina.Standard) },
                    { "onprem-std3", new EspecificacionesInstancia(8, 16, TipoMaquina.Standard) },
                    
                    { "onprem-mem1", new EspecificacionesInstancia(2, 16, TipoMaquina.OptimizadaMemoria) },
                    { "onprem-mem2", new EspecificacionesInstancia(4, 32, TipoMaquina.OptimizadaMemoria) },
                    { "onprem-mem3", new EspecificacionesInstancia(8, 64, TipoMaquina.OptimizadaMemoria) },
                    
                    { "onprem-cpu1", new EspecificacionesInstancia(2, 2, TipoMaquina.OptimizadaDisco) },
                    { "onprem-cpu2", new EspecificacionesInstancia(4, 4, TipoMaquina.OptimizadaDisco) },
                    { "onprem-cpu3", new EspecificacionesInstancia(8, 8, TipoMaquina.OptimizadaDisco) }
                }
            }
        };
        }
        public EspecificacionesInstancia ObtenerEspecificacionesInstancia(ProveedorNube proveedor, string tipoInstancia)
        {
            if (!_catalogo.ContainsKey(proveedor))
            {
                throw new ArgumentException($"Proveedor {proveedor} no soportado");
            }

            if (!_catalogo[proveedor].ContainsKey(tipoInstancia))
            {
                throw new ArgumentException($"Tipo de instancia {tipoInstancia} no existe para {proveedor}");
            }

            return _catalogo[proveedor][tipoInstancia];
        }

        public Dictionary<string, object> ObtenerInstanciasDisponibles(ProveedorNube proveedor)
        {
            if (!_catalogo.ContainsKey(proveedor))
            {
                throw new ArgumentException($"Proveedor {proveedor} no soportado");
            }

            var resultado = new Dictionary<string, object>();

            foreach (var categoria in _catalogo[proveedor].GroupBy(x => x.Value.TipoMaquina))
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
    public record EspecificacionesInstancia(int VCpus, int MemoryGB, TipoMaquina TipoMaquina);
}
