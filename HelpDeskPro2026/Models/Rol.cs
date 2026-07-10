using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
    }
}