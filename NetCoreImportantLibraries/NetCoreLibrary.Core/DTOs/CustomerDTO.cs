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


        #region CreditCard Class Property
        //CreditCard -> Class imi, Number -> Class içindeki property ismi
        public string CreditCardNumber { get; set; }

        //CreditCard -> Class imi, ValidityDate -> Class içindeki property ismi
        public DateTime CreditCardValidityDate { get; set; }
        
        /// <summary>
        /// Eğer AutoMapper'ın isimlendirme kurallarına uymazsam yani property başına class ismi vermez isem
        /// bunu Profile classı içerisinde belirtmeliyim.
        /// </summary>
        public int CVV { get; set; }
        #endregion
    }
}
