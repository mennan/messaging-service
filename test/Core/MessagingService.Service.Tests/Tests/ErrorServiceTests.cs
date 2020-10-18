using System;
using System.Linq;
using System.Threading.Tasks;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;
using Moq;
using Xunit;

namespace MessagingService.Service.Tests
{
    public class ErrorServiceTests
    {
        [Fact]
        public async Task Save_GivenInvalidModel_ReturnsErrorMessage()
        {
            var errorRepository = new Mock<IRepository<Error>>();
            var auditRepository = new Mock<IRepository<Audit>>();
            var service = new ErrorService(errorRepository.Object, auditRepository.Object);
            var errorModel = new ErrorDto();
            var result = await service.Save(errorModel);

            Assert.False(result.Success);
            Assert.Equal("Error message is required.", result.Errors.First());
        }

        [Fact]
        public async Task Save_GivenValidModel_ReturnsSuccess()
        {
            var errorRepository = new Mock<IRepository<Error>>();
            var auditRepository = new Mock<IRepository<Audit>>();

            errorRepository.Setup(x => x.Create(It.IsAny<Error>()));

            var service = new ErrorService(errorRepository.Object, auditRepository.Object);
            var errorModel = new ErrorDto()
                {Message = "Object reference not set to an instance of an object", StackTrace = ""};
            var result = await service.Save(errorModel);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Error saved successfully.", result.Message);
        }
    }
}