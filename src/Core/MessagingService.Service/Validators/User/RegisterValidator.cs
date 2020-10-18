using System.Threading.Tasks;
using FluentValidation;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IRepository<User> _userRepository;

        public RegisterValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(c => c.Name).MustAsync((name, cancellation) => UniqueName(name))
                .WithMessage("User name has been taken before another user.");
        }

        private async Task<bool> UniqueName(string name)
        {
            var isUserExist = await _userRepository.GetOne(x => x.Name == name);
            return isUserExist == null;
        }
    }
}