using Application.DTOs.Respuestas;
using Application.DTOs.Solicitudes;
using Application.Interfaces;
using Application.Mapeadores;
using Application.Servicios.Interfaces;
using Application.Validadores;
using Domain.Entidades;
using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servicios.Implementaciones
{
    public class ServicioAprovisionamiento : IServicioAprovisionamiento
    {
        private readonly IRegistroPrototipos _registroPrototipos;
        private readonly IRepositorioMaquinasVirtuales _repositorio;
        private readonly IConfiguracionInstancias _configuracion;
        private readonly ValidadorCoherenciaRecursos _validador;
        private readonly MapeadorRecursos _mapeador;

        public ServicioAprovisionamiento(
            IRegistroPrototipos registroPrototipos,
            IRepositorioMaquinasVirtuales repositorio,
            IConfiguracionInstancias configuracion,
            ValidadorCoherenciaRecursos validador,
            MapeadorRecursos mapeador)
        {
            _registroPrototipos = registroPrototipos;
            _repositorio = repositorio;
            _configuracion = configuracion;
            _validador = validador;
            _mapeador = mapeador;
        }

        public async Task<RespuestaMaquinaVirtualDto> CrearMaquinaVirtual(SolicitudCrearMaquinaVirtualDto solicitud)
        {
            var prototipo = _registroPrototipos.ObtenerPrototipo(solicitud.Proveedor, solicitud.TipoMaquina);

            var vmClonada = (MaquinaVirtual)prototipo.Clonar();

            PersonalizarMaquinaVirtual(vmClonada, solicitud);

            _validador.ValidarMaquinaVirtual(vmClonada);

            await _repositorio.Agregar(vmClonada);

            return _mapeador.MapearADto(vmClonada);
        }

        private void PersonalizarMaquinaVirtual(MaquinaVirtual vm, SolicitudCrearMaquinaVirtualDto solicitud)
        {
            if (!string.IsNullOrWhiteSpace(solicitud.TipoInstancia))
            {
                var specs = _configuracion.ObtenerEspecificaciones(solicitud.Proveedor, solicitud.TipoInstancia);
                vm.TipoInstancia = solicitud.TipoInstancia;
                vm.VCpus = specs.VCpus;
                vm.MemoryGB = specs.MemoryGB;
            }

            if (!string.IsNullOrWhiteSpace(solicitud.KeyPairName))
            {
                vm.KeyPairName = solicitud.KeyPairName;
            }

            vm.Red.Region = solicitud.Red.Region;
            vm.Red.FirewallRules = solicitud.Red.FirewallRules;
            vm.Red.PublicIP = solicitud.Red.PublicIP;
            vm.Red.Proveedor = solicitud.Proveedor;

            vm.Almacenamiento.Region = solicitud.Almacenamiento.Region;
            vm.Almacenamiento.Iops = solicitud.Almacenamiento.Iops;
            vm.Almacenamiento.Proveedor = solicitud.Proveedor;
        }

        public async Task<List<RespuestaMaquinaVirtualDto>> ObtenerTodasLasMaquinas()
        {
            var vms = await _repositorio.ObtenerTodas();
            return _mapeador.MapearListaADto(vms);
        }

        public async Task<RespuestaMaquinaVirtualDto?> ObtenerMaquinaPorId(string id)
        {
            var vm = await _repositorio.ObtenerPorId(id);
            return vm != null ? _mapeador.MapearADto(vm) : null;
        }

        public async Task<Dictionary<string, object>> ObtenerTiposInstanciaDisponibles(string proveedor)
        {
            if (!Enum.TryParse<ProveedorNube>(proveedor, true, out var proveedorEnum))
            {
                throw new ArgumentException($"Proveedor '{proveedor}' no válido");
            }

            return await Task.FromResult(_configuracion.ObtenerInstanciasDisponibles(proveedorEnum));
        }
    }
}
