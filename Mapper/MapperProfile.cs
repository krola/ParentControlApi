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

        CreateMap<Session, SessionDTO>();
        CreateMap<GetAllSessionsParams, Session>();
        CreateMap<GetDateSessionsParams, Session>();
        CreateMap<CreateSessionParams, Session>();
        CreateMap<UpdateSessionParams, Session>();

        CreateMap<Timesheet, TimesheetDTO>();
        CreateMap<GetTimesheetParams, Timesheet>();
        CreateMap<CreateTimesheetParams, Timesheet>();
        CreateMap<UpdateTimesheetParams, Timesheet>();
    }
}