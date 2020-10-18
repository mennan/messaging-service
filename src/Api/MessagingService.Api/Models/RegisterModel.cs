using System.ComponentModel.DataAnnotations;

namespace MessagingService.Api.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}