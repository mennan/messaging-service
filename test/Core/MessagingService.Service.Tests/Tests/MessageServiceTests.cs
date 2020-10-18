using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;
using Moq;
using Xunit;

namespace MessagingService.Service.Tests
{
    public class MessageServiceTests
    {
        private readonly Mock<IRepository<Error>> _errorRepository;
        private readonly Mock<IRepository<Audit>> _auditRepository;
        private readonly Mock<IRepository<User>> _userRepository;
        private readonly Mock<IRepository<Message>> _messageRepository;

        public MessageServiceTests()
        {
            _errorRepository = new Mock<IRepository<Error>>();
            _auditRepository = new Mock<IRepository<Audit>>();
            _userRepository = new Mock<IRepository<User>>();
            _messageRepository = new Mock<IRepository<Message>>();
        }

        #region SendMessage

        [Theory]
        [ClassData(typeof(SendMessageFailStubs))]
        public async Task SendMessage_GivenInvalidModel_ReturnsErrorMessage(SendMessageDto data)
        {
            var service = new MessageService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.SendMessage(data);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.NotNull(result.Errors);
            Assert.Equal("Model validation errors!", result.Message);
        }

        [Theory]
        [ClassData(typeof(SendMessageBlockedUserStubs))]
        public async Task SendMessage_UserBlocked_ReturnsErrorMessage(IQueryable<User> users, SendMessageDto data)
        {
            _userRepository.Setup(c => c.GetOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                (Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate));

            var service = new MessageService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.SendMessage(data);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Null(result.Errors);
            Assert.Equal("User blocked you!", result.Message);
        }

        [Theory]
        [ClassData(typeof(SendMessageSuccessStubs))]
        public async Task SendMessage_ValidModel_ReturnsSuccess(IQueryable<User> users, SendMessageDto data)
        {
            _userRepository.Setup(c => c.GetOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                (Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate));

            var service = new MessageService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.SendMessage(data);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Null(result.Errors);
            Assert.Equal("Message sent successfully.", result.Message);
        }

        #endregion

        #region GetAllMessages

        [Theory]
        [ClassData(typeof(GetAllMessagesFailStubs))]
        public async Task GetAllMessages_GivenInvalidModel_ReturnsErrorMessage(ReadUserMessageDto data)
        {
            var service = new MessageService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.GetAllMessages(data);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.NotNull(result.Errors);
            Assert.Equal("Model validation errors!", result.Message);
        }
        
        [Theory]
        [ClassData(typeof(GetAllMessagesSuccessStubs))]
        public async Task GetAllMessages_GivenValidModel_ReturnsSuccess(IQueryable<Message> messages, ReadUserMessageDto data)
        {
            _messageRepository.Setup(c => c.Get(It.IsAny<Expression<Func<Message, bool>>>())).ReturnsAsync(
                (Expression<Func<Message, bool>> predicate) => messages.Where(predicate).ToList());
            
            var service = new MessageService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.GetAllMessages(data);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Null(result.Errors);
            Assert.Equal("Messages listed successfully.", result.Message);
        }

        #endregion
        
        #region GetUnreadMessages

        [Theory]
        [ClassData(typeof(GetUnreadMessagesFailStubs))]
        public async Task GetUnreadMessages_GivenInvalidModel_ReturnsErrorMessage(ReadUserMessageDto data)
        {
            var service = new MessageService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.GetUnreadMessages(data);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.NotNull(result.Errors);
            Assert.Equal("Model validation errors!", result.Message);
        }
        
        [Theory]
        [ClassData(typeof(GetUnreadMessagesSuccessStubs))]
        public async Task GetUnreadMessages_GivenValidModel_ReturnsSuccess(IQueryable<Message> messages, ReadUserMessageDto data)
        {
            _messageRepository.Setup(c => c.Get(It.IsAny<Expression<Func<Message, bool>>>())).ReturnsAsync(
                (Expression<Func<Message, bool>> predicate) => messages.Where(predicate).ToList());
            
            var service = new MessageService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.GetUnreadMessages(data);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Null(result.Errors);
            Assert.Equal("Messages listed successfully.", result.Message);
        }

        #endregion
    }
}