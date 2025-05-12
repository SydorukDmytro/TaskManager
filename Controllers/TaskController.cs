using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.dto;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int projectId)
        {
            var tasks = await _service.GetTasksByProjectAsync(projectId);
            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            return task is null ? NotFound() : View(task);
        }

        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.CreateTaskAsync(dto);
            return RedirectToAction("Index", new { projectId = dto.ProjectId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            return task is null ? NotFound() : View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var success = await _service.UpdateTaskAsync(dto);
            return success ? RedirectToAction("Index", new { projectId = dto.ProjectId }) : NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            return task is null ? NotFound() : View(task);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int projectId)
        {
            var success = await _service.DeleteTaskAsync(id);
            return success ? RedirectToAction("Index", new { projectId }) : NotFound();
        }
    }
}
