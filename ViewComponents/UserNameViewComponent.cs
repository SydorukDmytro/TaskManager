using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public UserNameViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return View<string>("Default", null);

            var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
            var fullName = user?.FullName ?? user?.Email;

            return View("Default", fullName);
        }
    }
}
