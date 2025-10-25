using Application.DTOs.Respuestas;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapeadores
{
    public class MapeadorRecursos
    {
        public RespuestaMaquinaVirtualDto MapearADto(MaquinaVirtual vm)
        {
            return new RespuestaMaquinaVirtualDto
            {
                Id = vm.Id,
                Proveedor = vm.Provider,
                TipoMaquina = vm.TipoMaquina,
                TipoInstancia = vm.TipoInstancia,
                VCpus = vm.VCpus,
                MemoryGB = vm.MemoryGB,
                MemoryOptimization = vm.MemoryOptimization,
                DiskOptimization = vm.DiskOptimization,
                KeyPairName = vm.KeyPairName,
                Red = new DetalleRedDto
                {
                    Region = vm.Red.Region,
                    FirewallRules = vm.Red.FirewallRules,
                    PublicIP = vm.Red.PublicIP
                },
                Almacenamiento = new DetalleAlmacenamientoDto
                {
                    Region = vm.Almacenamiento.Region,
                    Iops = vm.Almacenamiento.Iops
                }
            };
        }

        public List<RespuestaMaquinaVirtualDto> MapearListaADto(List<MaquinaVirtual> vms)
        {
            return vms.Select(MapearADto).ToList();
        }
    }
}
