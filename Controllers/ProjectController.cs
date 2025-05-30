using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.dto;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _service;
        private readonly UserManager<User> userManager;

        public ProjectController(IProjectService service, UserManager<User> _userManager)
        {
            userManager = _userManager;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var projects = await _service.GetAllProjectsAsync();
            //if(User.IsInRole("Admin"))
            //{
                return View(projects);
            //}

            //var filtered = projects.Where(p => p.CreatedByUserId == user.Id).ToList();
            //return View(filtered);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetProjectByIdAsync(id);
            var user = await userManager.GetUserAsync(User);

            //if (result.CreatedByUserId == user.Id || User.IsInRole("Admin"))
            //{
            return View(result);
            //}

            //return NotFound();
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProjectDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var user = await userManager.GetUserAsync(User);
            dto.CreatedByUserId = user.Id;
            
            await _service.CreateProjectAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var project = await _service.GetProjectByIdAsync(id);
            if(user.Id == project.CreatedByUserId || User.IsInRole("Admin"))
            {
                var model = new ProjectDto
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    CreatedAt = project.CreatedAt,
                    CreatedByUserId = project.CreatedByUserId,
                    CreatedByUserName = project.CreatedByUserName
                };
                ViewBag.User = user;
                return View(model);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var success = await _service.UpdateProjectAsync(dto);
            return success ? RedirectToAction(nameof(Index)) : NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            var user = await userManager.GetUserAsync(User);
            if (project == null || (project.CreatedByUserId != user.Id && !User.IsInRole("Admin")))
                return Forbid();
            return project is null ? NotFound() : View(project);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            var user = await userManager.GetUserAsync(User);
            if (project == null || (project.CreatedByUserId != user.Id && !User.IsInRole("Admin")))
                return Forbid();
            var success = await _service.DeleteProjectAsync(id);
            return success ? RedirectToAction(nameof(Index)) : NotFound();
        }
    }

}
