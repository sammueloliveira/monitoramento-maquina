namespace MonitoramentoMaquinas.Domain.Entities
{
     public class TelemetryData
     {
        public string MachineId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double RPM { get; set; }
    }
}
