using MonitoramentoMaquinas.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MonitoramentoMaquinas.Domain.Entities
{
    public class Machine
    {
        [Key]
        public Guid Id { get; set; } 

        [Required]
        [StringLength(100, ErrorMessage = "O nome da máquina deve ter no máximo 50 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200, ErrorMessage = "A localização deve ter no máximo 50 caracteres.")]
        public string Location { get; set; } = string.Empty;
        public double? Latitude { get; set; }  
        public double? Longitude { get; set; } 

        public EnumStatus Status { get; set; }

       
    }
}
