using MonitoramentoMaquinas.Domain.Entities;

public class MachineStatusNotifier
{
    private readonly WebSocketHandler _webSocketHandler;

    public MachineStatusNotifier(WebSocketHandler webSocketHandler)
    {
        _webSocketHandler = webSocketHandler;
    }

    public async Task NotifyMachineStatusChange(Machine machine)
    {
       
        await _webSocketHandler.SendMessageToAllAsync($"Máquina {machine.Name} alterada para {machine.Status}");
    }
}
