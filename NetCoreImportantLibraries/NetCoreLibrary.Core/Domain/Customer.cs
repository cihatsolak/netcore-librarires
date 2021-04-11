using System;
using System.Collections.Generic;

namespace NetCoreLibrary.Core.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// Customer.Addresses[0] şeklinde kullanabilmek için IList interface'ini kullanıyorum.
        /// </summary>
        public IList<Address> Addresses { get; set; }
    }
}
