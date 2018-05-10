public class DeviceNotExistException : BaseNotExistException
{
    const string Description = "Device not exist!";
    public DeviceNotExistException() : base(Description) { }
}