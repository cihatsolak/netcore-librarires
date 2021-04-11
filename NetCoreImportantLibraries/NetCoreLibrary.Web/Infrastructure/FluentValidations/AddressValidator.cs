using FluentValidation;
using NetCoreLibrary.Core.Domain;

namespace NetCoreLibrary.Web.Infrastructure.FluentValidations
{
    /// <summary>
    /// Address class'ı için validasyon
    /// </summary>
    public class AddressValidator : AbstractValidator<Address>
    {
        public string NotEmptyMassage { get; } = "{PropertyName} alanı boş olamaz.";

        public AddressValidator()
        {
            RuleFor(p => p.Content).NotEmpty().WithMessage(NotEmptyMassage);

            RuleFor(p => p.Province).NotEmpty().WithMessage(NotEmptyMassage);

            RuleFor(p => p.PostCode).NotEmpty().WithMessage(NotEmptyMassage)
                                    .MaximumLength(5).WithMessage("Posta kodu en fazla {MaxLength} karakter olabilir.");
        }
    }
}
