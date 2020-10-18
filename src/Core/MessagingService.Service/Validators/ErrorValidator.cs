using FluentValidation;
using MessagingService.Dto;

namespace MessagingService.Service
{
    public class ErrorValidator : AbstractValidator<ErrorDto>
    {
        public ErrorValidator()
        {
            RuleFor(c => c.Message).NotEmpty().WithMessage("Error message is required.");
        }
    }
}