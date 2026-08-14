using FluentValidation;

namespace OS.Application.Operations.Auth.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Foydalanuvchi nomi kiritilishi shart.")
                .MinimumLength(3).WithMessage("Foydalanuvchi nomi kamida 3 ta belgidan iborat bo'lishi kerak.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email kiritilishi shart.")
                .EmailAddress().WithMessage("To'g'ri email manzilini kiriting.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parol kiritilishi shart.")
                .MinimumLength(6).WithMessage("Parol kamida 6 ta belgidan iborat bo'lishi kerak.");
        }
    }
}
