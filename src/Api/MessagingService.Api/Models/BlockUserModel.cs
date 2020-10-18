using System.ComponentModel.DataAnnotations;

namespace MessagingService.Api.Models
{
    public class BlockUserModel
    {
        [Required]
        public string UserName { get; set; }
    }
}