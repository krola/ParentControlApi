using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

public interface ISessionService
{
    IEnumerable<Session> GetForDay(int deviceId, DateTime date);
    IEnumerable<Session> GetAll(int deviceId);
    void Create(Session session);

    void Update(Session newSession);
}

public class SessionService : ISessionService{
    private readonly IDeviceService deviceService;
    private readonly IRepository<Session> sessionRepositor;

    public SessionService(IDeviceService deviceService, IRepository<Session> sessionRepositor){
        this.deviceService = deviceService;
        this.sessionRepositor = sessionRepositor;
    }

    public void Create(Session session)
    {
        sessionRepositor.Add(session);
    }

    public IEnumerable<Session> GetAll(int deviceId)
    {
        var device = deviceService.Get(deviceId);
        return sessionRepositor.FindBy(t => t.Device == device).AsEnumerable();
    }

    public IEnumerable<Session> GetForDay(int deviceId, DateTime date)
    {
        var device = deviceService.Get(deviceId);
        return sessionRepositor.FindBy(t => t.Device == device && t.StarTime.Date == date.Date).AsEnumerable();
    
    }

    public void Update(Session newSession)
    {
        var oldSession = sessionRepositor.FindBy(t => t.Id == newSession.Id).SingleOrDefault();
        if(oldSession == null){
            Create(newSession);
        }
        var updatedSession = Mapper.Map<Session, Session>(newSession, oldSession);
        sessionRepositor.Edit(updatedSession);
    }
}