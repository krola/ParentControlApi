using Newtonsoft.Json;

public class BasePocket {
    public string Type { get; set; }
}

public class RequestPocket : BasePocket
{
        public Header Header { get; set; }
}

public abstract class Response<T> : BasePocket where T: class {
        public abstract T Data {get; set;}
}