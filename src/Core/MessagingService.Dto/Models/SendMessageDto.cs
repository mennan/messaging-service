namespace MessagingService.Dto
{
    public class SendMessageDto
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Content { get; set; }
    }
}