public class SessionNotExistsException : BaseAlreadyExistException
{
    const string Description = "Session with that Id doesn't exist. Please create one before!";
    public SessionNotExistsException() : base(Description) { }
}