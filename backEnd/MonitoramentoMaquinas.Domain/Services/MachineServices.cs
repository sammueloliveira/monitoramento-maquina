using MonitoramentoMaquinas.Domain.Entities;
using MonitoramentoMaquinas.Domain.Interfaces;
using MonitoramentoMaquinas.Domain.InterfaceServices;

namespace MonitoramentoMaquinas.Domain.Services
{
    public class MachineServices : IMachineServices
     {
        private readonly IMachine _machine;
        public MachineServices(IMachine machine) 
        {
            _machine = machine;
        }

        public async Task AddMachine(Machine machine)
        {
            machine.Id = Guid.NewGuid();

            await _machine.Add(machine);
        }

        public async Task UpdateMachine(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine), "A máquina não pode ser nula.");

            var machineExists = await _machine.GetEntityById(machine.Id);

            if (machineExists != null)
            {
                machineExists.Name = machine.Name;
                machineExists.Location = machine.Location;
                machineExists.Latitude = machine.Latitude;
                machineExists.Longitude = machine.Longitude;
                machineExists.Status = machine.Status;

                await _machine.Update(machineExists);
            }
            else
            {
                throw new KeyNotFoundException("Máquina não encontrada.");
            }
        }
     }
}

