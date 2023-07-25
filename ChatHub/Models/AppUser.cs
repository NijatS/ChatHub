using Microsoft.AspNetCore.Identity;

namespace ChatHub.Models
{
    public class AppUser:IdentityUser
    {
        public string? ConnectionId { get; set; }
    }
}
