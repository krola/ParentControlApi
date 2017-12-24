public class ScheduleAlreadyExistsException : System.Exception
{
    const string Message = "Schedule with that name already exists for that device!";
    public ScheduleAlreadyExistsException() : base(Message) { }
}