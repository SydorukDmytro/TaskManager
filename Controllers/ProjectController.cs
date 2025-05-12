using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.dto;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllProjectsAsync();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetProjectByIdAsync(id);
            return result is null ? NotFound() : View(result);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProjectDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.CreateProjectAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            return project is null ? NotFound() : View(project);
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
            return project is null ? NotFound() : View(project);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _service.DeleteProjectAsync(id);
            return success ? RedirectToAction(nameof(Index)) : NotFound();
        }
    }

}
