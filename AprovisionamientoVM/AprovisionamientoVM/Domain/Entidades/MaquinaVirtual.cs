using Domain.Enumeraciones;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class MaquinaVirtual : IPrototypeMaquinaVirtual
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public ProveedorNube Provider { get; set; }
        public int VCpus { get; set; }
        public int MemoryGB { get; set; }
        public bool MemoryOptimization { get; set; }
        public bool DiskOptimization { get; set; }
        public string? KeyPairName { get; set; }
        public TipoMaquina TipoMaquina { get; set; }
        public string TipoInstancia { get; set; } = string.Empty;

        public Red Red { get; set; } = new Red();
        public Almacenamiento Almacenamiento { get; set; } = new Almacenamiento();



        public IPrototypeMaquinaVirtual Clonar()
        {
            return new MaquinaVirtual
            {
                Id = Guid.NewGuid().ToString(),
                Provider = this.Provider,
                VCpus = this.VCpus,
                MemoryGB = this.MemoryGB,
                MemoryOptimization = this.MemoryOptimization,
                DiskOptimization = this.DiskOptimization,
                KeyPairName = this.KeyPairName,
                TipoMaquina = this.TipoMaquina,
                TipoInstancia = this.TipoInstancia,
                Red = this.Red.Clonar(),
                Almacenamiento = this.Almacenamiento.Clonar()
            };
        }

        public void Validar()
        {
            if (VCpus <= 0)
            {
                throw new Excepciones.ValidacionRecursoExcepcion("El número de vCPUs debe ser mayor a 0");
            }

            if (MemoryGB <= 0)
            {
                throw new Excepciones.ValidacionRecursoExcepcion("La memoria RAM debe ser mayor a 0 GB");
            }

            if (string.IsNullOrWhiteSpace(TipoInstancia))
            {
                throw new Excepciones.ValidacionRecursoExcepcion("El tipo de instancia es obligatorio");
            }

            Red.Validar();
            Almacenamiento.Validar();
        }

        public void ValidarCoherenciaProveedor()
        {
            if (Red.Proveedor != this.Provider)
            {
                throw new Excepciones.IncoherenciaProveedorExcepcion(
                    $"La red pertenece al proveedor {Red.Proveedor} pero la VM es de {this.Provider}");
            }

            if (Almacenamiento.Proveedor != this.Provider)
            {
                throw new Excepciones.IncoherenciaProveedorExcepcion(
                    $"El almacenamiento pertenece al proveedor {Almacenamiento.Proveedor} pero la VM es de {this.Provider}");
            }
        }

        public void ValidarCoherenciaRegion()
        {
            if (Red.Region != Almacenamiento.Region)
            {
                throw new Excepciones.IncoherenciaProveedorExcepcion(
                    $"La red está en la región {Red.Region} pero el almacenamiento en {Almacenamiento.Region}");
            }
        }
    }
}
