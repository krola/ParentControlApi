public class ScheduleNotExistsException : BaseNotExistException
{
    const string Description = "Schedule not exist!";
    public ScheduleNotExistsException() : base(Description) { }
}