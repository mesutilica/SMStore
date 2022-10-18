using FluentValidation; // FluentValidation ı kullanabilmek için
using SMStore.Entities;

namespace SMStore.Service.ValidationRules
{
    public class AppUserValidator : AbstractValidator<AppUser> // AbstractValidator FluentValidation ın kontrol sınıfıdır
    {
        public AppUserValidator()
        {
            // Buradaki constructor da validasyon kurallarını belirliyoruz
            RuleFor(x => x.Name).NotEmpty(); // Kontrol ettiğimiz AppUser clasının Name özelliğinin boş olmaması gerktiğini belirttik
            RuleFor(x => x.Surname).NotNull(); // Soyadı boş olamaz
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş Bırakılamaz!");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre Boş Bırakılamaz!").MinimumLength(3).WithMessage("Şifre Minimum 3 Karakter Olmalıdır!");
            // Burada rulefor ile tüm kurallarımızı koyduktan sonra bu AppUserValidator classını program.cs de servis olarak tanımlıyoruz.
        }
    }
}
