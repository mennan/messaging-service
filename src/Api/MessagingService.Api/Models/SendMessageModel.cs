using System.ComponentModel.DataAnnotations;

namespace MessagingService.Api.Models
{
    public class SendMessageModel
    {
        [Required]
        public string To { get; set; }
        
        [Required]
        public string Content { get; set; }
    }
}