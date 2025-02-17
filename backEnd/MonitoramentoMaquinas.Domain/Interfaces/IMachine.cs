using MonitoramentoMaquinas.Domain.Entities;
using MonitoramentoMaquinas.Domain.Enum;

namespace MonitoramentoMaquinas.Domain.Interfaces
{
    public interface IMachine : IGeneric<Machine>
    {
        Task<IEnumerable<Machine>> GetMachinesByStatus(EnumStatus status);
    }
}
