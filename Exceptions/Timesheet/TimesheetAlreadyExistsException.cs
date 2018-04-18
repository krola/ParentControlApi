public class TimesheetNonAlreadyExistsException : BaseAlreadyExistException
{
    const string Message = "Timesheet with that Id doesn't exist. Please create one before!";
    public TimesheetNonAlreadyExistsException() : base(Message) { }
}