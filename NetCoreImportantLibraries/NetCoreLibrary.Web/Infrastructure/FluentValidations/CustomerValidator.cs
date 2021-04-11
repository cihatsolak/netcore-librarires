using FluentValidation;
using NetCoreLibrary.Core.Domain;
using System;

namespace NetCoreLibrary.Web.Infrastructure.FluentValidations
{
    /// <summary>
    /// Customer class'ı için validasyon
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public string NotEmptyMassage { get; set; } = "{PropertyName} alanı boş olamaz.";

        /// <summary>
        /// InclusiveBetween: 18-60 aralığında olmalı
        /// </summary>
        public CustomerValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(NotEmptyMassage);

            RuleFor(p => p.Email).NotEmpty().WithMessage(NotEmptyMassage)
                                 .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(p => p.Age).NotEmpty().WithMessage(NotEmptyMassage)
                               .InclusiveBetween(18, 60).WithMessage("Yaş alanı 18-60 arasında olmalıdır.");

            //Must: CustomValidator.
            RuleFor(p => p.BirthDay).NotEmpty().WithMessage(NotEmptyMassage)
                                    .Must(birthDay =>
                                    {
                                        return DateTime.Now.AddYears(-18) >= birthDay;
                                    }).WithMessage("Yaşınız 18 yaşından büyük olmalıdır.");
        }
    }
}
