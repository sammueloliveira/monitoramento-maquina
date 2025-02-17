using Microsoft.EntityFrameworkCore;
using MonitoramentoMaquinas.Domain.Entities;
using MonitoramentoMaquinas.Domain.Enum;
using MonitoramentoMaquinas.Domain.Interfaces;
using MonitoramentoMaquinas.Infra.Data;

namespace MonitoramentoMaquinas.Infra.Repositories
{
     public class RepositoryMachine : RepositoryGeneric<Machine>, IMachine
     {
        private readonly Context _context;
        public RepositoryMachine(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Machine>> GetMachinesByStatus(EnumStatus status)
        {
            return await _context.Machines
                .Where(m => m.Status == status)
                .ToListAsync();
        }
     }
}
