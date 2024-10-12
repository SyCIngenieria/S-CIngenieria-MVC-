using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace S_CIngenieria.Models.GestionContratos
{
    public class OrdenCambio
    {
        public int Id { get; set; }

        [ForeignKey("ODS")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ODSId { get; set; }
        public ODS? ODS { get; set; }

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
