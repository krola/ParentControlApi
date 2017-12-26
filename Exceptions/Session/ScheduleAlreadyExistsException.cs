public class SessionNotExistsException : System.Exception
{
    const string Message = "Session with that Id doesn't exist. Please create one before!";
    public SessionNotExistsException() : base(Message) { }
}