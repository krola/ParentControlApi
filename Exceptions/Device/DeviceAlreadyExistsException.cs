public class DeviceAlreadyExistsException : BaseAlreadyExistException
{
    const string Description = "Device with that name already exists for that user!";
    public DeviceAlreadyExistsException() : base(Description) { }
}