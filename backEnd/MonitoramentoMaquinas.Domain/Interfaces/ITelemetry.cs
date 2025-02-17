using MonitoramentoMaquinas.Domain.Entities;

namespace MonitoramentoMaquinas.Domain.Interfaces
{
     public interface ITelemetry
     {
        Task StartTelemetrySimulation(string machineId);
        Task StopTelemetrySimulation();
     }
}
