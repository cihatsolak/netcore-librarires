using System;

namespace NetCoreLibrary.Core.Domain
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
