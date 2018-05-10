public class TimesheetNonExistsException : BaseNotExistException
{
    const string Description = "Timesheet not exist";
    public TimesheetNonExistsException() : base(Description) { }
}