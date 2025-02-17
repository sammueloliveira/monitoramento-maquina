using AutoMapper;
using MonitoramentoMaquinas.Domain.Entities;
using MonitoramentoMaquinas.Presentation.Models.DTOs;

namespace MonitoramentoMaquinas.Presentation.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Machine, MachineDTO>().ReverseMap();

        }
    }
}
