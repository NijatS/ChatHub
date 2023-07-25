using ChatHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatHub.Context
{
    public class ChatHubDbContext : IdentityDbContext<AppUser>
    {
        public ChatHubDbContext(DbContextOptions<ChatHubDbContext> options) : base(options)
        {
        }
    }
}
