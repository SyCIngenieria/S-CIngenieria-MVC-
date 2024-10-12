using System.ComponentModel.DataAnnotations;

namespace S_CIngenieria.Models.GestionContratos
{
    public class Proyectos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string NombreProyecto { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string DescripcionProyecto { get; set; }

        public ICollection<ODS>? ODS { get; set; }
    }
}
