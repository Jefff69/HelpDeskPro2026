using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        // Relación: Un rol puede tener muchos usuarios
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}