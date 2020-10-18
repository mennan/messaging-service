using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class MessageService : BaseService, IMessageService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;

        public MessageService(IRepository<User> userRepository, IRepository<Message> messageRepository,
            IRepository<Error> errorRepository, IRepository<Audit> auditRepository) : base(errorRepository,
            auditRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<ServiceResponse<bool>> SendMessage(SendMessageDto model)
        {
            try
            {
                var validator = new SendMessageValidator(_userRepository);
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "Model validation errors!",
                        Errors = GetValidationErrors(validationResult)
                    };
                }

                var toUser = await model.ToUser.GetUser(_userRepository);

                toUser.BlockedUsers ??= new List<string>();
                if (toUser.BlockedUsers.Contains(model.FromUser))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "User blocked you!"
                    };
                }

                var message = new Message
                {
                    FromUser = model.FromUser,
                    ToUser = model.ToUser,
                    Content = model.Content,
                    IsRead = false,
                    ReadDate = DateTime.MinValue,
                    SentDate = DateTime.UtcNow
                };

                await _messageRepository.Create(message);
                await LogAudit(AuditTypes.MessageSend, model.FromUser, nameof(MessageService));

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Message sent successfully."
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

        public async Task<ServiceResponse<PagedUserMessageDto>> GetAllMessages(ReadUserMessageDto model)
        {
            try
            {
                var validator = new ReadMessageValidator();
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new ServiceResponse<PagedUserMessageDto>
                    {
                        Success = false,
                        Message = "Model validation errors!",
                        Errors = GetValidationErrors(validationResult)
                    };
                }

                var startIndex = (model.Page - 1) * model.PerPage;
                var userMessages =
                    await _messageRepository.Get(x => x.FromUser == model.Name || x.ToUser == model.Name);
                var count = userMessages.Count;
                var messages = MapUserMessageData(userMessages.Skip(startIndex).Take(model.PerPage).ToList());
                var totalPages = Convert.ToInt32(Math.Ceiling((double) count / model.PerPage));
                var returnData = new PagedUserMessageDto
                {
                    CurrentPage = model.Page,
                    PerPage = model.PerPage,
                    TotalPage = totalPages,
                    Messages = messages
                };

                await LogAudit(AuditTypes.MessageRead, model.Name, nameof(MessageService));

                return new ServiceResponse<PagedUserMessageDto>
                {
                    Success = true,
                    Data = returnData,
                    Message = "Messages listed successfully."
                };
            }
            catch (Exception ex)
            {
                await LogError(ex);

                return new ServiceResponse<PagedUserMessageDto>
                {
                    Success = false,
                    Message = Constants.DefaultErrorMessage
                };
            }
        }

        public async Task<ServiceResponse<PagedUserMessageDto>> GetUnreadMessages(ReadUserMessageDto model)
        {
            try
            {
                var validator = new ReadMessageValidator();
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new ServiceResponse<PagedUserMessageDto>
                    {
                        Success = false,
                        Message = "Model validation errors!",
                        Errors = GetValidationErrors(validationResult)
                    };
                }

                var startIndex = (model.Page - 1) * model.PerPage;
                var userMessages =
                    await _messageRepository.Get(x =>
                        (x.FromUser == model.Name || x.ToUser == model.Name) && !x.IsRead);
                var count = userMessages.Count;
                var messages = MapUserMessageData(userMessages.Skip(startIndex).Take(model.PerPage).ToList());
                var totalPages = Convert.ToInt32(Math.Ceiling((double) count / model.PerPage));
                var returnData = new PagedUserMessageDto
                {
                    CurrentPage = model.Page,
                    PerPage = model.PerPage,
                    TotalPage = totalPages,
                    Messages = messages
                };

                await MarkAsReadMessages(userMessages);
                await LogAudit(AuditTypes.MessageRead, model.Name, nameof(MessageService));

                return new ServiceResponse<PagedUserMessageDto>
                {
                    Success = true,
                    Data = returnData,
                    Message = "Messages listed successfully."
                };
            }
            catch (Exception ex)
            {
                await LogError(ex);

                return new ServiceResponse<PagedUserMessageDto>
                {
                    Success = false,
                    Message = Constants.DefaultErrorMessage
                };
            }
        }

        #region Helpers

        private List<UserMessageDto> MapUserMessageData(List<Message> userMessages) =>
            userMessages.OrderByDescending(x => x.SentDate).Select(x => new UserMessageDto
            {
                From = x.FromUser,
                To = x.ToUser,
                Content = x.Content,
                IsRead = x.IsRead,
                ReadDate = x.ReadDate,
                SentDate = x.SentDate
            }).ToList();

        private async Task MarkAsReadMessages(List<Message> messages)
        {
            foreach (var message in messages)
            {
                message.IsRead = true;
                message.ReadDate = DateTime.UtcNow;

                await _messageRepository.Update(message);
            }
        }

        #endregion
    }
}