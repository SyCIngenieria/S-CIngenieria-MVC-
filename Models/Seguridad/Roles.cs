using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace S_CIngenieria.Models.Seguridad
{
    public class Roles
    {
        public int Id { get; set; }

        public enum Nombres
        {
            Administrador,
            Cliente,
            Empresa
        }
        public Nombres Nombre { get; set; }
        public bool Estado { get; set; } = true;
    }
}
