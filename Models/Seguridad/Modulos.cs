using System.ComponentModel.DataAnnotations;

namespace S_CIngenieria.Models.Seguridad
{
    public class Modulos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string NombreModulo { get; set; } = null!;

        public bool Estado { get; set; } = true;
    }
}
