public class DeviceNotExistException : System.Exception
{
    const string Message = "Device not exist!";
    public DeviceNotExistException() : base(Message) { }
}