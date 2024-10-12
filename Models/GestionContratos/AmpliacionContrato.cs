using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace S_CIngenieria.Models.GestionContratos
{
    public class AmpliacionContrato
    {
        public int Id { get; set; }

        [ForeignKey("Contrato")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ContratoId { get; set; }
        public Contrato? Contrato { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicioAmpliacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaFinAmpliacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Currency)]
        public decimal ValorAmpliacion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} debe ser un valor no negativo")]
        public int NumeroDiasAmpliacion { get; set; }
    }
}

