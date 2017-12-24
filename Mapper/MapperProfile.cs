using AutoMapper;
using ParentControlApi.DTO;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Device, DeviceDTO>();
        CreateMap<DeviceDTO, Device>();
        CreateMap<ScheduleDTO, Schedule>();
        CreateMap<Schedule, ScheduleDTO>();
    }
}