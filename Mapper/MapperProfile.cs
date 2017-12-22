using AutoMapper;
using ParentControlApi.DTO;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Device, DeviceDTO>();
        CreateMap<DeviceDTO, Device>();
    }
}