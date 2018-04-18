using System.Collections.Generic;

namespace ParentControlApi.DTO
{
    public class Authorization
    {
        public string Token { get; set; }
        public double Expires { get; set; }
    }
}
