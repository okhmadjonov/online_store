using FluentValidation;

namespace OS.Application.Operations.Auth.Commands.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Foydalanuvchi nomi yoki email kiritilishi shart.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parol kiritilishi shart.");
        }
    }
}
