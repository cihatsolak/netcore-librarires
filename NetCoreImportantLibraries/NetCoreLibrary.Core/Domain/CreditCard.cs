using System;

namespace NetCoreLibrary.Core.Domain
{
    /// <summary>
    /// AutoMapper Flattening (Complex type ile DTO nesnelerini mapleme)
    /// </summary>
    public class CreditCard
    {
        public string Number { get; set; }
        public DateTime ValidityDate { get; set; }
        public int CardValidationValue { get; set; }
    }
}
