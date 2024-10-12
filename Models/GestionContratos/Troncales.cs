using System.ComponentModel.DataAnnotations;

namespace S_CIngenieria.Models.GestionContratos
{
    public class Troncales
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string NombreTroncal { get; set; }

        public ICollection<ODS>? ODS { get; set; }
    }
}
