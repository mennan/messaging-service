using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public class BaseService
    {
        private readonly IRepository<Error> _errorRepository;
        private readonly IRepository<Audit> _auditRepository;

        public BaseService(IRepository<Error> errorRepository, IRepository<Audit> auditRepository)
        {
            _errorRepository = errorRepository;
            _auditRepository = auditRepository;
        }

        protected List<string> GetValidationErrors(ValidationResult result) =>
            result.Errors.Select(x => x.ErrorMessage).ToList();

        protected async Task LogAudit(AuditTypes auditType, string username, string service)
        {
            var audit = new Audit
            {
                User = username,
                Type = auditType.ToString(),
                Service = service,
                AuditDate = DateTime.UtcNow
            };

            await _auditRepository.Create(audit);
        }

        protected async Task LogError(Exception ex)
        {
            var error = new Error
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                ErrorDate = DateTime.UtcNow
            };

            await _errorRepository.Create(error);
        }
    }
}