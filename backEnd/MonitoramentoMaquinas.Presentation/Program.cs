using Microsoft.EntityFrameworkCore;
using MonitoramentoMaquinas.Domain.Interfaces;
using MonitoramentoMaquinas.HelpConfig.HelpStartup;
using MonitoramentoMaquinas.Infra.Data;
using MonitoramentoMaquinas.Infra.WebSockets;
using MonitoramentoMaquinas.Presentation.Models.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionMySql = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<Context>(options =>
options.UseMySql(connectionMySql, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));

HelpStartup.ConfigureScoped(builder.Services);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSingleton<WebSocketHandler>();

builder.Services.AddSingleton<MachineStatusNotifier>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.WithOrigins("http://localhost:4200") 
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.UseWebSockets();
app.UseMiddleware<WebSocketMiddleware>();

app.MapControllers();

app.Run();
