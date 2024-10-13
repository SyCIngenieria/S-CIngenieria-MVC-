using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace S_CIngenieria.Models.Seguridad
{
    public class Usuarios
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; } = null!;

        [DataType(DataType.Url)]
        public string? fotoPerfil { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string Apellidos { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo válida")]
        public string Correo { get; set; } = null!;

        [Phone(ErrorMessage = "El campo {0} no es un número de teléfono válido")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaExpira { get; set; }

        [ForeignKey("Rol")]
        public int FkRol { get; set; }
        public Roles? Rol { get; set; }
    }
}
