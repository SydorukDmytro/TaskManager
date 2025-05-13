using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.dto;
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
            var users = await userManager.Users.ToListAsync();
            var model = new List<UserWithRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                model.Add(new UserWithRolesViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    RoleName = roles.FirstOrDefault() ?? "—"
                });
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null) return NotFound();
            var roles = await roleManager.Roles.ToListAsync();
            var currentRoles = await userManager.GetRolesAsync(user);

            return View(new UserWithRolesViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = currentRoles.FirstOrDefault(),
                AvailableRoles = roles.Select(role => new SelectListItem { Text = role.Name, Value = role.Name }).ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserWithRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id.ToString());
            if (user is null) return NotFound();
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.NormalizedUserName = model.Email.ToUpper();

            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);
            await userManager.AddToRoleAsync(user, model.RoleName);
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await userManager.FindByIdAsync(id.ToString());
            var role = await userManager.GetRolesAsync(result);
            if (result == null) return NotFound();
            return View(new UserDto
            {
                Id = result.Id,
                FullName = result.FullName,
                Email = result.Email,
                RoleName = role.FirstOrDefault()
            });
        }
    }

}
