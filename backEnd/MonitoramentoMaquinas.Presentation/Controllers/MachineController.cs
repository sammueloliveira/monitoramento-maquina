using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoMaquinas.Domain.Entities;
using MonitoramentoMaquinas.Domain.Enum;
using MonitoramentoMaquinas.Domain.Interfaces;
using MonitoramentoMaquinas.Domain.InterfaceServices;
using MonitoramentoMaquinas.Presentation.Models.DTOs;

namespace MonitoramentoMaquinas.Presentation.Controllers
{
    [Route("api/machine")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachine _machine;
        private readonly IMachineServices _machineServices;
        private readonly MachineStatusNotifier _notifier;
        private readonly ITelemetry _telemetry;
        private readonly IMapper _mapper;

        public MachineController(IMachine machine, IMachineServices machineServices, MachineStatusNotifier notifier, ITelemetry telemetry, IMapper mapper)
        {
            _machine = machine;
            _machineServices = machineServices;
            _notifier = notifier;
            _telemetry = telemetry;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMachines()
        {
            var machines = await _machine.List();
            return Ok(machines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMachineById(Guid id)
        {
            var machine = await _machine.GetEntityById(id);
            if (machine == null)
            {
                return NotFound("Máquina não encontrada.");
            }
            return Ok(machine);
        }

        [HttpPost]
        public async Task<IActionResult> AddMachine([FromBody] MachineDTO machineDto)
        {
            if (machineDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            if (!Enum.IsDefined(typeof(EnumStatus), machineDto.Status))
            {
                return BadRequest("Status inválido. Os valores permitidos são: 1 (Operando), 2 (Parada para Manutenção) e 3 (Desligada).");
            }

           
            var machine = _mapper.Map<Machine>(machineDto);

            await _machineServices.AddMachine(machine);

            await _notifier.NotifyMachineStatusChange(machine);

            return CreatedAtAction(nameof(GetMachineById), new { id = machine.Id }, machine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(Guid id, [FromBody] MachineDTO machineDto)
        {
            
            if (!Enum.IsDefined(typeof(EnumStatus), machineDto.Status))
            {
                return BadRequest("Status inválido. Os valores permitidos são: 1 (Operando), 2 (Parada para Manutenção) e 3 (Desligada).");
            }

            var existingMachine = await _machine.GetEntityById(id);
            if (existingMachine == null)
            {
                return NotFound("Máquina não encontrada.");
            }

          
            _mapper.Map(machineDto, existingMachine);

            await _machineServices.UpdateMachine(existingMachine);

            await _notifier.NotifyMachineStatusChange(existingMachine);

            return Ok(existingMachine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(Guid id)
        {
            var machine = await _machine.GetEntityById(id);
            if (machine == null)
            {
                return NotFound("Máquina não encontrada.");
            }

            await _machine.Delete(machine);
            return NoContent();
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetMachinesByStatus(EnumStatus status)
        {
            try
            {
                var machines = await _machine.GetMachinesByStatus(status);

                if (machines == null || !machines.Any())
                {
                    return NotFound("Nenhuma máquina encontrada com esse status.");
                }

                return Ok(machines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        [HttpPost("start/{machineId}")]
        public async Task<IActionResult> StartSimulation(string machineId)
        {
            try
            {
               
                _ = Task.Run(() => _telemetry.StartTelemetrySimulation(machineId));

                return Ok($"Simulação de telemetria iniciada para a máquina {machineId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao iniciar simulação: {ex.Message}");
            }
        }

        [HttpPost("stop/{machineId}")]
        public IActionResult StopSimulation(string machineId)
        {
            try
            {
               
                _telemetry.StopTelemetrySimulation();
                return Ok($"Simulação de telemetria parada para a máquina {machineId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao parar simulação: {ex.Message}");
            }
        }



    }
}
