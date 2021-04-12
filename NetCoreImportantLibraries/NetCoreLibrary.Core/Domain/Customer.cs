using NetCoreLibrary.Core.Enums;
using System;
using System.Collections.Generic;

namespace NetCoreLibrary.Core.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }

        public Gender Gender { get; set; }

        /// <summary>
        /// Customer.Addresses[0] şeklinde kullanabilmek için IList interface'ini kullanıyorum.
        /// </summary>
        public IList<Address> Addresses { get; set; }

        /// <summary>
        /// AutoMapper tarafında bu metot ile bir property'i maplemek amacıyla oluşturdum.
        /// Eğer metot ismi ile maplenecek property ismi aynı olursa (Örn: GetFullName() ) direk map işlemi olur. Aynı olmaz ise bunu profile tarafında belirtmeliyiz.
        /// </summary>
        /// <returns></returns>
        public string NameAndLastName() //GetFullName() yazarsak direk eşlenir.
        {
            return string.Concat(Name, " ", LastName);
        }

        /// <summary>
        /// AutoMapper ile complex type mapleme için oluşturuldu.
        /// CustomerDTO class ının property'lerine maplenecek.
        /// </summary>
        public CreditCard CreditCard { get; set; }
    }
}
