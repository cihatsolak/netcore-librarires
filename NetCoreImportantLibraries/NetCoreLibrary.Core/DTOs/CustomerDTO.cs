using System;

namespace NetCoreLibrary.Core.DTOs
{
    public class CustomerDTO
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
