using MonitoramentoMaquinas.Domain.Entities;
using MonitoramentoMaquinas.Domain.Interfaces;

namespace MonitoramentoMaquinas.Infra.WebSockets
{
    public class Telemetry : ITelemetry
    {
        private readonly IMachine _machine;
        private readonly WebSocketHandler _webSocketHandler;
        private readonly Random _random = new();
        private CancellationTokenSource _cancellationTokenSource;

        public Telemetry(WebSocketHandler webSocketHandler, IMachine machine)
        {
            _webSocketHandler = webSocketHandler;
            _machine = machine;
          
        }
        public async Task StartTelemetrySimulation(string machineId)
        {
            var machine = await _machine.GetEntityById(Guid.Parse(machineId));
            if (machine == null)
            {
                throw new Exception("Máquina não encontrada");
            }

            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            while (!token.IsCancellationRequested)
            {
                var telemetryData = GenerateTelemetryData(machineId);

                await _webSocketHandler.SendMessageToAllAsync($"Máquina {machineId}: Temp={telemetryData.Temperature}°C, Pressão={telemetryData.Pressure}bar, RPM={telemetryData.RPM}");

                await Task.Delay(5000, token); 
            }
        }
        public Task StopTelemetrySimulation()
        {
            _cancellationTokenSource?.Cancel();  
            return Task.CompletedTask; 
        }
        private TelemetryData GenerateTelemetryData(string machineId)
        {
            return new TelemetryData
            {
                MachineId = machineId,
                Timestamp = DateTime.Now,
                Temperature = _random.Next(60, 90),   
                Pressure = _random.Next(5, 10),      
                RPM = _random.Next(1200, 2400)        
            };
        }

 
    }
}
