public class TimesheetNonExistsException : BaseNotExistException
{
    const string Message = "Timesheet not exist";
    public TimesheetNonExistsException() : base(Message) { }
}