using System.Collections.Generic;

namespace MessagingService.Entity
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> BlockedUsers { get; set; }
    }
}