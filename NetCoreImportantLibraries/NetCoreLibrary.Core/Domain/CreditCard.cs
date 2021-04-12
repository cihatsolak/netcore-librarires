using System;

namespace NetCoreLibrary.Core.Domain
{
    public class CreditCard
    {
        public string Number { get; set; }
        public DateTime ValidityDate { get; set; }
        public int CardValidationValue { get; set; }
    }
}
