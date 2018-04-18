public class DeviceNotExistException : BaseNotExistException
{
    const string Message = "Device not exist!";
    public DeviceNotExistException() : base(Message) { }
}