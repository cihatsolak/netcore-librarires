using System;

namespace NetCoreLibrary.Core.DTOs
{
    public class CustomerDTO
    {
        /// <summary>
        /// Tam Adı (isim + soyisim)
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// İsim
        /// </summary>
        public string Name { get; set; }
        public string Mail { get; set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
