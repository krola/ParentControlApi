public class TimesheetNonAlreadyExistsException : BaseAlreadyExistException
{
    const string Description = "Timesheet with that Id doesn't exist. Please create one before!";
    public TimesheetNonAlreadyExistsException() : base(Description) { }
}