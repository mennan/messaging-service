using System;

namespace MessagingService.Entity
{
    public class Error : BaseEntity
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime ErrorDate { get; set; }
    }
}