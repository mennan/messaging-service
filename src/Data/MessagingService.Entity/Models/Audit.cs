using System;

namespace MessagingService.Entity
{
    public class Audit : BaseEntity
    {
        public string Type { get; set; }
        public string User { get; set; }
        public string Service { get; set; }
        public DateTime AuditDate { get; set; }
    }
}