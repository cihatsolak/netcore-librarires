using FluentValidation;
using NetCoreLibrary.Core.Domain;

namespace NetCoreLibrary.Web.Infrastructure.FluentValidations
{
    /// <summary>
    /// Customer class'ı için validasyon
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>
    {
        /// <summary>
        /// InclusiveBetween: 18-60 aralığında olmalı
        /// </summary>
        public CustomerValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email alanı boş olamaz.")
                                 .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(p => p.Age).NotEmpty().WithMessage("Yaş alanı boş olamaz.")
                .InclusiveBetween(18, 60).WithMessage("Yaş alanı 18-60 arasında olmalıdır.");
        }
    }
}
