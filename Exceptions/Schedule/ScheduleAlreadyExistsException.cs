public class ScheduleAlreadyExistsException : BaseAlreadyExistException
{
    const string Description = "Schedule with that name already exists for that device!";
    public ScheduleAlreadyExistsException() : base(Description) { }
}