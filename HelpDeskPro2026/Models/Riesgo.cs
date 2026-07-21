using HelpDeskPro2026.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.Models
{
    public class Riesgo : CatalogoBase
    {
        [Required(ErrorMessage = "El color es obligatorio.")]
        [StringLength(20, ErrorMessage = "El color no puede superar los 20 caracteres.")]
        public string Color { get; set; } = string.Empty;
    }
}