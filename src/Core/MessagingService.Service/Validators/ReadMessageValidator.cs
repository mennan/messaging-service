using FluentValidation;
using MessagingService.Dto;

namespace MessagingService.Service
{
    public class ReadMessageValidator : AbstractValidator<ReadUserMessageDto>
    {
        public ReadMessageValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.PerPage).Must(x => x >= 1).WithMessage("Per page must be greater than 0.");
            RuleFor(c => c.Page).Must(x => x >= 1).WithMessage("Page must be greater than 0.");
        }
    }
}