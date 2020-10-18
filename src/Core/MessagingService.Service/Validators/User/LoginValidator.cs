using System.Data;
using FluentValidation;
using MessagingService.Dto;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}