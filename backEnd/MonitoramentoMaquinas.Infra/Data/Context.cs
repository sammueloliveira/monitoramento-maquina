using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoramentoMaquinas.Domain.Entities;

namespace MonitoramentoMaquinas.Infra.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Machine> Machines { get; set; }
        
    }
}
