using ChatHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ChatHub.Hubs
{
    public class ChattHub:Hub
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public ChattHub(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task SendMessage(string userId, string message)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId);
            if (appUser is not null)
            {
                string user = _contextAccessor.HttpContext.User.Identity.Name;
                if(!string.IsNullOrWhiteSpace(appUser.ConnectionId)) {
                    await Clients.Client(appUser.ConnectionId).SendAsync("ReceiveMessage",user, message);
                }
            }
        }
        public async override Task OnConnectedAsync()
        {
            string connid = Context.ConnectionId;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser? appUser = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);

                appUser.ConnectionId = connid;
                await _userManager.UpdateAsync(appUser);
                await Clients.All.SendAsync("Loggin", appUser.Id);
            }

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser? appUser = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);

                appUser.ConnectionId = null;

                await _userManager.UpdateAsync(appUser);
                await Clients.All.SendAsync("Logout", appUser.Id);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
