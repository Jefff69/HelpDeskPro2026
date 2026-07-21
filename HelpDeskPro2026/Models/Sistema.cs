using HelpDeskPro2026.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.Models
{
    public class Sistema : CatalogoBase
    {
        [Required(ErrorMessage = "El código del sistema es obligatorio.")]
        [StringLength(10, ErrorMessage = "El código no puede superar los 10 caracteres.")]
        public string Codigo { get; set; } = string.Empty;
    }
}