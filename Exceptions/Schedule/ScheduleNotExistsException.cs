public class ScheduleNotExistsException : BaseNotExistException
{
    const string Message = "Schedule not exist!";
    public ScheduleNotExistsException() : base(Message) { }
}