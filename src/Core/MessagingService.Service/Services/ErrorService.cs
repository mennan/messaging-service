using System;
using System.Threading.Tasks;
using MessagingService.Dto;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class ErrorService : BaseService, IErrorService
    {
        private readonly IRepository<Error> _errorRepository;

        public ErrorService(IRepository<Error> errorRepository, IRepository<Audit> auditRepository) : base(
            errorRepository, auditRepository)
        {
            _errorRepository = errorRepository;
        }

        public async Task<ServiceResponse<bool>> Save(ErrorDto model)
        {
            var validator = new ErrorValidator();
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

            var data = new Error
            {
                Message = model.Message,
                StackTrace = model.StackTrace,
                ErrorDate = DateTime.UtcNow
            };

            await _errorRepository.Create(data);

            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Error saved successfully."
            };
        }
    }
}