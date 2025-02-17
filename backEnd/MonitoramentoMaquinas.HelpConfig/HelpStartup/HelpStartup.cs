using Microsoft.Extensions.DependencyInjection;
using MonitoramentoMaquinas.Domain.Interfaces;
using MonitoramentoMaquinas.Domain.InterfaceServices;
using MonitoramentoMaquinas.Domain.Services;
using MonitoramentoMaquinas.Infra.Repositories;
using MonitoramentoMaquinas.Infra.WebSockets;

namespace MonitoramentoMaquinas.HelpConfig.HelpStartup
{
    public static class HelpStartup
    {
        public static void ConfigureScoped(IServiceCollection services)
        {
            // Interfaces e Repositorios
            services.AddScoped(typeof(IGeneric<>), typeof(RepositoryGeneric<>));
            services.AddScoped<IMachine, RepositoryMachine>();
            services.AddScoped<ITelemetry, Telemetry>();

            // Serviço  Dominio
            services.AddScoped<IMachineServices, MachineServices>();
          
        }
    }
}
