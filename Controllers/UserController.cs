using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UserController(IUserService service, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _service = service;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _service.GetAllUsersAsync();
            var roles = roleManager.Roles.ToList();
            var model = users.Select(user => new UserWithRolesViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = user.RoleName
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetUserByIdAsync(id);
            return result is null ? NotFound() : View(result);
        }
    }

}
