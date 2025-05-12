using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllUsersAsync();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetUserByIdAsync(id);
            return result is null ? NotFound() : View(result);
        }
    }

}
