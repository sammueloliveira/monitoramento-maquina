using MonitoramentoMaquinas.Domain.Enum;

namespace MonitoramentoMaquinas.Presentation.Models.DTOs
{
    public class MachineDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public double? Latitude { get; set; }  
        public double? Longitude { get; set; } 
        public EnumStatus Status { get; set; }
    }
}
