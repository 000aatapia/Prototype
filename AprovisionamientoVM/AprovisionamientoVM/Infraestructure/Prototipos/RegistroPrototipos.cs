using Domain.Entidades;
using Domain.Enumeraciones;
using Infraestructure.Configuraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Prototipos
{
    public class RegistroPrototipos
    {
        private readonly Dictionary<string, MaquinaVirtual> _prototipos;
        private readonly ConfiguracionInstanciasPorProveedor _configuracion;

        public RegistroPrototipos(ConfiguracionInstanciasPorProveedor configuracion)
        {
            _configuracion = configuracion;
            _prototipos = new Dictionary<string, MaquinaVirtual>();
            InicializarPrototipos();
        }

        private void InicializarPrototipos()
        {
            RegistrarPrototiposAWS();

            RegistrarPrototiposAzure();

            RegistrarPrototiposGCP();

            RegistrarPrototiposOnPremise();
        }

        private void RegistrarPrototiposAWS()
        {
            RegistrarPrototipo(new PrototipoMaquinaStandard(
                ProveedorNube.AWS, "t3.medium", 2, 4));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaDisco(
                ProveedorNube.AWS, "c5.large", 2, 4));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaMemoria(
                ProveedorNube.AWS, "r5.large", 2, 16));
        }

        private void RegistrarPrototiposAzure()
        {
            RegistrarPrototipo(new PrototipoMaquinaStandard(
                ProveedorNube.Azure, "D2s_v3", 2, 8));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaDisco(
                ProveedorNube.Azure, "F2s_v2", 2, 4));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaMemoria(
                ProveedorNube.Azure, "E2s_v3", 2, 16));
        }

        private void RegistrarPrototiposGCP()
        {
            RegistrarPrototipo(new PrototipoMaquinaStandard(
                ProveedorNube.GCP, "e2-standard-2", 2, 8));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaDisco(
                ProveedorNube.GCP, "n2-highcpu-2", 2, 2));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaMemoria(
                ProveedorNube.GCP, "n2-highmem-2", 2, 16));
        }

        private void RegistrarPrototiposOnPremise()
        {
            RegistrarPrototipo(new PrototipoMaquinaStandard(
                ProveedorNube.OnPremise, "onprem-std1", 2, 4));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaDisco(
                ProveedorNube.OnPremise, "onprem-cpu1", 2, 2));

            RegistrarPrototipo(new PrototipoMaquinaOptimizadaMemoria(
                ProveedorNube.OnPremise, "onprem-mem1", 2, 16));
        }
        private void RegistrarPrototipo(MaquinaVirtual prototipo)
        {
            var clave = GenerarClave(prototipo.Provider, prototipo.TipoMaquina);
            _prototipos[clave] = prototipo;
        }
        public MaquinaVirtual ObtenerPrototipo(ProveedorNube proveedor, TipoMaquina tipoMaquina)
        {
            var clave = GenerarClave(proveedor, tipoMaquina);

            if (!_prototipos.ContainsKey(clave))
            {
                throw new KeyNotFoundException(
                    $"No existe prototipo para Proveedor={proveedor}, TipoMaquina={tipoMaquina}");
            }

            return _prototipos[clave];
        }
        private string GenerarClave(ProveedorNube proveedor, TipoMaquina tipoMaquina)
        {
            return $"{proveedor}_{tipoMaquina}";
        }
        public Dictionary<string, MaquinaVirtual> ObtenerTodosLosPrototipos()
        {
            return new Dictionary<string, MaquinaVirtual>(_prototipos);
        }
    }
}
