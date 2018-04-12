using AutoMapper;
using ParentControlApi.DTO;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<DeviceDTO, DeviceDTO>();
        CreateMap<Device, DeviceDTO>();
        CreateMap<CreateDeviceParams, Device>();
        CreateMap<CreateDeviceParams, Device>();

        CreateMap<Schedule, ScheduleDTO>();
        CreateMap<GetSchedulesParams, Schedule>();
        CreateMap<CreateScheduleParams, Schedule>();
        CreateMap<UpdateScheduleParams, Schedule>();

        CreateMap<Session, SessionDTO>()
            .ForMember(dest => dest.SessionStart, opt => opt.MapFrom(src => src.StarTime))
            .ForMember(dest => dest.SessionEnd, opt => opt.MapFrom(src => src.EndTime));
        CreateMap<GetDateSessionsParams, Session>();
        CreateMap<CreateSessionParams, Session>()
            .ForMember(dest => dest.StarTime, opt => opt.MapFrom(src => src.SessionStart))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.SessionEnd));
        CreateMap<UpdateSessionParams, Session>()
            .ForMember(dest => dest.StarTime, opt => opt.MapFrom(src => src.SessionStart))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.SessionEnd));

        CreateMap<Timesheet, TimesheetDTO>();
        CreateMap<GetTimesheetParams, Timesheet>();
        CreateMap<CreateTimesheetParams, Timesheet>();
        CreateMap<UpdateTimesheetParams, Timesheet>();
    }
}