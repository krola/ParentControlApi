using System.Collections.Generic;
using System.Linq;

public class SessionRepository 
        : GenericRepository<ParentControlContext, Session>, ISessionRepository  {
    public List<Session> GetAllSessions() => GetAll().ToList();
}