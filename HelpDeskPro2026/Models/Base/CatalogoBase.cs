using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.Models.Base
{
    public abstract class CatalogoBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}