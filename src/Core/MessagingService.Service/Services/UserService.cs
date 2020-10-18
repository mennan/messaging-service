using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;

        public UserService(IRepository<User> userRepository, IRepository<Message> messageRepository,
            IRepository<Error> errorRepository, IRepository<Audit> auditRepository) : base(errorRepository,
            auditRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<ServiceResponse<bool>> Register(RegisterDto model)
        {
            try
            {
                var validator = new RegisterValidator(_userRepository);
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "User not created!",
                        Errors = GetValidationErrors(validationResult)
                    };
                }

                var password = model.Password.Encrypt();

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = password
                };

                await _userRepository.Create(user);
                await LogAudit(AuditTypes.Register, model.Name, nameof(UserService));

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "User created successfully"
                };
            }
            catch (Exception ex)
            {
                await LogError(ex);

                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = Constants.DefaultErrorMessage
                };
            }
        }

        public async Task<ServiceResponse<string>> Login(LoginDto model)
        {
            try
            {
                var validator = new LoginValidator();
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "Model validation errors!",
                        Errors = GetValidationErrors(validationResult)
                    };
                }

                await LogAudit(AuditTypes.LoginAttempt, model.Name, nameof(UserService));

                var password = model.Password.Encrypt();
                var user = await _userRepository.GetOne(x => x.Name == model.Name && x.Password == password);

                if (user == null)
                {
                    await LogAudit(AuditTypes.InvalidLogin, model.Name, nameof(UserService));

                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "Username or password is invalid!"
                    };
                }

                await LogAudit(AuditTypes.Login, model.Name, nameof(UserService));

                return new ServiceResponse<string>
                {
                    Success = true,
                    Data = user.Id,
                    Message = "User found."
                };
            }
            catch (Exception ex)
            {
                await LogError(ex);

                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = Constants.DefaultErrorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> BlockUser(BlockUserDto model)
        {
            try
            {
                var validator = new BlockUserValidator(_userRepository);
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Model validation errors!",
                        Errors = GetValidationErrors(validationResult)
                    };
                }

                var blockedByUser = await model.BlockedByUser.GetUser(_userRepository);

                blockedByUser.BlockedUsers ??= new List<string>();

                if (blockedByUser.BlockedUsers.Contains(model.BlockedUser))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "User has been before blocked!"
                    };
                }

                blockedByUser.BlockedUsers.Add(model.BlockedUser);
                await _userRepository.Update(blockedByUser);

                await LogAudit(AuditTypes.BlockUser, model.BlockedByUser, nameof(UserService));

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "User blocked successfully."
                };
            }
            catch (Exception ex)
            {
                await LogError(ex);

                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = Constants.DefaultErrorMessage
                };
            }
        }
    }
}