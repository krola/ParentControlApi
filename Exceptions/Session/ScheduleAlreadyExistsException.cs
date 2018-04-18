public class SessionNotExistsException : BaseAlreadyExistException
{
    const string Message = "Session with that Id doesn't exist. Please create one before!";
    public SessionNotExistsException() : base(Message) { }
}