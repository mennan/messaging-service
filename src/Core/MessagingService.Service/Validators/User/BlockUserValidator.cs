using System.Threading.Tasks;
using FluentValidation;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class BlockUserValidator : AbstractValidator<BlockUserDto>
    {
        private readonly IRepository<User> _userRepository;

        public BlockUserValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(c => c.BlockedByUser).NotNull().WithMessage("Blocked by user name is required.");
            RuleFor(c => c.BlockedUser).NotNull().WithMessage("Blocked user name is required.");
            RuleFor(c => c.BlockedByUser).MustAsync((name, cancellation) => ExistUser(name))
                .WithMessage("Blocked by user not found.");
            RuleFor(c => c.BlockedUser).MustAsync((name, cancellation) => ExistUser(name))
                .WithMessage("Blocked user not found.");
        }

        private async Task<bool> ExistUser(string name)
        {
            var isUserExist = await _userRepository.GetOne(x => x.Name == name);
            return isUserExist != null;
        }
    }
}