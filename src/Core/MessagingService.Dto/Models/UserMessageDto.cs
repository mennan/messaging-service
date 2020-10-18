using System;
using System.Collections.Generic;

namespace MessagingService.Dto
{
    public class PagedUserMessageDto
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PerPage { get; set; }
        public List<UserMessageDto> Messages { get; set; }
    }

    public class UserMessageDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime SentDate { get; set; }
    }
}