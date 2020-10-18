using System;
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
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _userRepository;
        private readonly Mock<IRepository<Message>> _messageRepository;
        private readonly Mock<IRepository<Error>> _errorRepository;
        private readonly Mock<IRepository<Audit>> _auditRepository;

        public UserServiceTests()
        {
            _userRepository = new Mock<IRepository<User>>();
            _messageRepository = new Mock<IRepository<Message>>();
            _errorRepository = new Mock<IRepository<Error>>();
            _auditRepository = new Mock<IRepository<Audit>>();
        }

        #region Register

        [Theory]
        [ClassData(typeof(RegisterFailStubs))]
        public async Task Register_GivenInvalidModel_ReturnsErrorMessage(IQueryable<User> users, RegisterDto data)
        {
            _userRepository.Setup(c => c.GetOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                (Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate));

            var service = new UserService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.Register(data);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.NotNull(result.Errors);
            Assert.Equal("User not created!", result.Message);
        }

        [Theory]
        [ClassData(typeof(RegisterSuccessStubs))]
        public async Task Register_GivenValidModel_ReturnsSuccess(IQueryable<User> users, RegisterDto data)
        {
            _userRepository.Setup(c => c.GetOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                (Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate));

            var service = new UserService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.Register(data);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Null(result.Errors);
            Assert.Equal("User created successfully", result.Message);
        }

        #endregion

        #region Login

        [Theory]
        [ClassData(typeof(LoginFailStubs))]
        public async Task Login_GivenInvalidModel_ReturnsErrorMessage(IQueryable<User> users, LoginDto data)
        {
            _userRepository.Setup(c => c.GetOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                (Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate));

            var service = new UserService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.Login(data);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.NotNull(result.Errors);
            Assert.Equal("Model validation errors!", result.Message);
        }
        
        [Theory]
        [ClassData(typeof(LoginInvalidCredentialStubs))]
        public async Task Login_GivenInvalidCredential_ReturnsErrorMessage(IQueryable<User> users, LoginDto data)
        {
            _userRepository.Setup(c => c.GetOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                (Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate));

            var service = new UserService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.Login(data);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Username or password is invalid!", result.Message);
        }
        
        [Theory]
        [ClassData(typeof(LoginValidCredentialStubs))]
        public async Task Login_GivenValidCredential_ReturnsErrorMessage(IQueryable<User> users, LoginDto data)
        {
            _userRepository.Setup(c => c.GetOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                (Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate));

            var service = new UserService(_userRepository.Object, _messageRepository.Object, _errorRepository.Object,
                _auditRepository.Object);

            var result = await service.Login(data);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("User found.", result.Message);
        }

        #endregion
    }
}