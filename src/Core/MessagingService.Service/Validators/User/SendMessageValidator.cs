using System.Threading.Tasks;
using FluentValidation;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class SendMessageValidator : AbstractValidator<SendMessageDto>
    {
        private readonly IRepository<User> _userRepository;
        
        public SendMessageValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            
            RuleFor(c => c.FromUser).NotEmpty().WithMessage("From user name is required.");
            RuleFor(c => c.ToUser).NotEmpty().WithMessage("To user name is required.");
            RuleFor(c => c.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(c => c.FromUser).MustAsync((name, cancellation) => ExistUser(name))
                .WithMessage("From user not found!");
            RuleFor(c => c.ToUser).MustAsync((name, cancellation) => ExistUser(name))
                .WithMessage("To user not found!");
        }
        
        private async Task<bool> ExistUser(string name)
        {
            var isUserExist = await _userRepository.GetOne(x => x.Name == name);
            return isUserExist != null;
        }
    }
}