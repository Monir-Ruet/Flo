using Flo.Web.DTOs.Identity;
using FluentValidation;

namespace Flo.Web.Validators.Auth;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}