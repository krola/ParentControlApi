public class DeviceAlreadyExistsException : System.Exception
{
    const string Message = "Device with that name already exists for that user!";
    public DeviceAlreadyExistsException() : base(Message) { }
}