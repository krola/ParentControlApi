public class ScheduleAlreadyExistsException : BaseAlreadyExistException
{
    const string Message = "Schedule with that name already exists for that device!";
    public ScheduleAlreadyExistsException() : base(Message) { }
}