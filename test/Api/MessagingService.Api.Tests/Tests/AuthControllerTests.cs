using System;
using System.Collections.Generic;
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
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IJwtHelper> _jwtHelper;

        public AuthControllerTests()
        {
            _userService = new Mock<IUserService>();
            _jwtHelper = new Mock<IJwtHelper>();
        }
        
        #region Register
        
        [Theory]
        [ClassData(typeof(RegisterSuccessStubs))]
        public async Task Register_ReturnsSuccessMessage(RegisterModel registerModel)
        {
            // Arrange
            var expectedResult = new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "User created successfully"
            };

            _userService.Setup(x => x.Register(It.IsAny<RegisterDto>())).ReturnsAsync(expectedResult);

            var controller = new AuthController(_userService.Object, _jwtHelper.Object);

            // Act
            var result = await controller.Register(registerModel);

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<bool>>(apiResult.Value);

            Assert.True(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status200OK, apiResult.StatusCode);
        }

        [Theory]
        [ClassData(typeof(RegisterFailStubs))]
        public async Task Register_ReturnsFailMessage(RegisterModel registerModel)
        {
            // Arrange
            var expectedResult = new ServiceResponse<bool>
            {
                Success = false,
                Message = "User not created!",
                Errors = new List<string> {"User name has been taken before another user."}
            };

            _userService.Setup(x => x.Register(It.IsAny<RegisterDto>())).ReturnsAsync(expectedResult);

            var controller = new AuthController(_userService.Object, _jwtHelper.Object);

            // Act
            var result = await controller.Register(registerModel);

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<List<string>>>(apiResult.Value);

            Assert.False(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.NotNull(model.Data);
            Assert.Single(model.Data);
            Assert.Equal(StatusCodes.Status400BadRequest, apiResult.StatusCode);
        }
        
        #endregion

        #region Login

        [Theory]
        [ClassData(typeof(LoginFailStubs))]
        public async Task Login_GivenInvalidCredential_ReturnsErrorMessage(LoginModel loginModel)
        {
            // Arrange
            var expectedResult = new ServiceResponse<string>
            {
                Success = false,
                Message = "Username or password is invalid!",
            };

            _userService.Setup(x => x.Login(It.IsAny<LoginDto>())).ReturnsAsync(expectedResult);

            var controller = new AuthController(_userService.Object, _jwtHelper.Object);

            // Act
            var result = await controller.Login(loginModel);

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<List<string>>>(apiResult.Value);

            Assert.False(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, apiResult.StatusCode);
        }
        
        [Theory]
        [ClassData(typeof(LoginSuccessStubs))]
        public async Task Login_GivenValidCredential_ReturnsJwtToken(LoginModel loginModel)
        {
            // Arrange
            var expectedResult = new ServiceResponse<string>
            {
                Success = true,
                Data = Guid.NewGuid().ToString(),
                Message = "Login successfully.",
            };

            _userService.Setup(x => x.Login(It.IsAny<LoginDto>())).ReturnsAsync(expectedResult);
            _jwtHelper.Setup(x => x.GetJwtSecret()).Returns(It.IsAny<Guid>().ToString());

            var controller = new AuthController(_userService.Object, _jwtHelper.Object);

            // Act
            var result = await controller.Login(loginModel);

            // Assert
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<LoginResponseModel>>(apiResult.Value);

            Assert.True(model.Success);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(StatusCodes.Status200OK, apiResult.StatusCode);
            Assert.NotNull(model.Data);
            Assert.NotNull(model.Data.AccessToken);
            Assert.Equal(3, model.Data.AccessToken.Split('.').Length);
        }
        
        #endregion
    }
}