using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingService.Api.Controllers;
using MessagingService.Api.Models;
using MessagingService.Dto;
using MessagingService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Enumerable = System.Linq.Enumerable;

namespace MessagingService.Api.Tests
{
    public class MessagesControllerTests
    {
        private readonly Mock<IMessageService> _messageService;

        public MessagesControllerTests()
        {
            _messageService = new Mock<IMessageService>();
        }
        
        #region AllMessages

        [Fact]
        public async Task AllMessages_GivenNullUserName_ReturnsErrorMessage()
        {
            // Arrange
            var expectedResult = new ServiceResponse<PagedUserMessageDto>
            {
                Success = false,
                Message = "Model validation errors!",
                Errors = new List<string> {"Name is required"}
            };

            _messageService.Setup(x => x.GetAllMessages(It.IsAny<ReadUserMessageDto>())).ReturnsAsync(expectedResult);

            var controller = new MessagesController(_messageService.Object);

            // Act
            var result = await controller.AllMessages();

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<List<string>>>(apiResult.Value);

            Assert.False(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, apiResult.StatusCode);
            Assert.NotNull(model.Data);
            Assert.Single(model.Data);
        }

        [Theory]
        [ClassData(typeof(AllMessagesSuccessStubs))]
        public async Task AllMessages_GivenValidUserName_ReturnsSuccess(
            ServiceResponse<PagedUserMessageDto> expectedResult)
        {
            // Arrange
            _messageService.Setup(x => x.GetAllMessages(It.IsAny<ReadUserMessageDto>())).ReturnsAsync(expectedResult);

            var controller = new MessagesController(_messageService.Object);

            // Act
            var result = await controller.AllMessages();

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<PagedApiData<UserMessageDto>>(apiResult.Value);

            Assert.True(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status200OK, apiResult.StatusCode);
            Assert.NotNull(model.Data);
            Assert.Single(model.Data);
        }

        #endregion

        #region UnreadMessages

        [Fact]
        public async Task UnreadMessages_GivenNullUserName_ReturnsErrorMessage()
        {
            // Arrange
            var expectedResult = new ServiceResponse<PagedUserMessageDto>
            {
                Success = false,
                Message = "Model validation errors!",
                Errors = new List<string> {"Name is required"}
            };

            _messageService.Setup(x => x.GetUnreadMessages(It.IsAny<ReadUserMessageDto>())).ReturnsAsync(expectedResult);

            var controller = new MessagesController(_messageService.Object);

            // Act
            var result = await controller.UnreadMessages();

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<List<string>>>(apiResult.Value);

            Assert.False(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, apiResult.StatusCode);
            Assert.NotNull(model.Data);
            Assert.Single(model.Data);
        }

        [Theory]
        [ClassData(typeof(UnreadMessagesSuccessStubs))]
        public async Task UnreadMessages_GivenValidUserName_ReturnsSuccess(
            ServiceResponse<PagedUserMessageDto> expectedResult)
        {
            // Arrange
            _messageService.Setup(x => x.GetUnreadMessages(It.IsAny<ReadUserMessageDto>())).ReturnsAsync(expectedResult);

            var controller = new MessagesController(_messageService.Object);

            // Act
            var result = await controller.UnreadMessages();

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<PagedApiData<UserMessageDto>>(apiResult.Value);

            Assert.True(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status200OK, apiResult.StatusCode);
            Assert.NotNull(model.Data);
            Assert.Single(model.Data);
        }

        #endregion

        #region SendMessage

        [Theory]
        [ClassData(typeof(SendMessageFailStubs))]
        public async Task SendMessage_GivenInvalidModel_ReturnsErrorMessage(ServiceResponse<bool> expectedResult,
            string expectedMessage)
        {
            // Arrange
            _messageService.Setup(x => x.SendMessage(It.IsAny<SendMessageDto>())).ReturnsAsync(expectedResult);

            var modelData = new SendMessageModel {To = "", Content = ""};
            var controller = new MessagesController(_messageService.Object);

            // Act
            var result = await controller.SendMessage(modelData);

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<List<string>>>(apiResult.Value);

            Assert.False(model.Success);
            Assert.Equal("Model validation errors!", model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, apiResult.StatusCode);
            Assert.NotNull(model.Data);
            Assert.Single(model.Data);
            Assert.Equal(expectedMessage, model.Data.First());
        }
        
        [Theory]
        [ClassData(typeof(SendMessageSuccessStubs))]
        public async Task SendMessage_GivenValidModel_ReturnsSuccess(ServiceResponse<bool> expectedResult)
        {
            // Arrange
            _messageService.Setup(x => x.SendMessage(It.IsAny<SendMessageDto>())).ReturnsAsync(expectedResult);

            var modelData = new SendMessageModel {To = "mennan", Content = "Hello!"};
            var controller = new MessagesController(_messageService.Object);

            // Act
            var result = await controller.SendMessage(modelData);

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<bool>>(apiResult.Value);

            Assert.True(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status200OK, apiResult.StatusCode);
            Assert.True(model.Data);
        }

        #endregion
    }
}