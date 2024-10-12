using S_CIngenieria.Models.Seguridad;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace S_CIngenieria.Models.GestionContratos
{
    public class ODS
    {
        public int Id { get; set; }

        [ForeignKey("Contrato")]
        public int ContratosId { get; set; }
        public Contrato? Contrato { get; set; }

        [ForeignKey("AmpliacionContrato")]
        public int AmpliacionContratoId { get; set; }
        public AmpliacionContrato? AmpliacionContrato { get; set; }

        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [DataType(DataType.Currency)]
        public int ValorInicalODS { get; set; }

        public int DiasEjecuCionODS { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicioODS { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaFinODS { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SuspensionFechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SuspensionFechaFin { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [DataType(DataType.Currency)]
        public decimal ValorActual { get; set; }

        public enum Estado
        {
            Pendiente,
            EnProceso,
            Completado,
            Cancelado
        }
        public Estado EstadoODS { get; set; }

        [ForeignKey("Usuarios")]
        public int Supervisor { get; set; }
        public Usuarios? SupervisorUsuarios { get; set; }

        [ForeignKey("Usuarios")]
        public int SolicitanteCliente { get; set; }
        public Usuarios? SolicitanteUsuarios { get; set; }

        public enum Recurso
        {
            OPEX,
            CAPEX,
            Mixto
        }
        public Recurso RecursoODS { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string PlantaSistema { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string ConexoObra { get; set; }

        [ForeignKey("Proyectos")]
        public int ProyectosId { get; set; }
        public Proyectos? Proyectos { get; set; }

        [ForeignKey("Troncales")]
        public int TroncalesId { get; set; }
        public Troncales? Troncales { get; set; }

        public ICollection<Actas>? Actas { get; set; }
    }
}
