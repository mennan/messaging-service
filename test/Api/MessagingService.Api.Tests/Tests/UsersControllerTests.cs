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

namespace MessagingService.Api.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userService;

        public UsersControllerTests()
        {
            _userService = new Mock<IUserService>();
        }
        
        #region BlockUser

        [Theory]
        [ClassData(typeof(BlockUserFailStubs))]
        public async Task BlockUser_GivenInvalidModel_ReturnsErrorMessage(ServiceResponse<bool> expectedResult,
            string expectedMessage)
        {
            // Arrange
            _userService.Setup(x => x.BlockUser(It.IsAny<BlockUserDto>())).ReturnsAsync(expectedResult);

            var modelData = new BlockUserModel {UserName = "anil"};
            var controller = new UsersController(_userService.Object);

            // Act
            var result = await controller.BlockUser(modelData);

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<List<string>>>(apiResult.Value);

            Assert.False(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, apiResult.StatusCode);
            Assert.NotNull(model.Data);
            Assert.Single(model.Data);
            Assert.Equal(expectedMessage, model.Data.First());
        }
        
        [Theory]
        [ClassData(typeof(BlockUserSuccessStubs))]
        public async Task BlockUser_GivenValidModel_ReturnsErrorMessage(ServiceResponse<bool> expectedResult)
        {
            // Arrange
            _userService.Setup(x => x.BlockUser(It.IsAny<BlockUserDto>())).ReturnsAsync(expectedResult);

            var modelData = new BlockUserModel {UserName = "mennan"};
            var controller = new UsersController(_userService.Object);

            // Act
            var result = await controller.BlockUser(modelData);

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