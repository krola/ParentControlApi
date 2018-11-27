using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using LinqKit;

public interface ISessionService
{
    IEnumerable<Session> Get(int deviceId, DateTime? from = null, DateTime? to = null);
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

    public IEnumerable<Session> Get(int deviceId, DateTime? from = null, DateTime? to = null)
    {
        var device = deviceService.Get(deviceId);
        var predicate = PredicateBuilder.New<Session>()
                        .And(s => s.Device == device);
        
        if(from.HasValue){
            predicate = predicate.And(t => t.StarTime.Date >= from.Value.Date);
        }
        if(to.HasValue){
            predicate = predicate.And(s => s.EndTime.HasValue && s.EndTime.Value.Date <= to.Value.Date);
        }

        return sessionRepositor.FindBy(predicate).AsEnumerable();
    
    }

    public void Update(Session newSession)
    {
        var oldSession = sessionRepositor.FindBy(t => t.Id == newSession.Id).SingleOrDefault();
        if(oldSession == null){
            Create(newSession);
        }
        
        oldSession.StarTime = newSession.StarTime;
        oldSession.EndTime = newSession.EndTime;

        sessionRepositor.Edit(oldSession);
    }
}