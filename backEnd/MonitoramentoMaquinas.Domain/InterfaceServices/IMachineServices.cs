using MonitoramentoMaquinas.Domain.Entities;

namespace MonitoramentoMaquinas.Domain.InterfaceServices
{
     public interface IMachineServices
     {
        Task AddMachine(Machine machine);
        Task UpdateMachine(Machine machine);
     }
}
