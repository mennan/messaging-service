using System;

namespace MessagingService.Entity
{
    public class Message : BaseEntity
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReadDate { get; set; }
    }
}