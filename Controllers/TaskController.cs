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
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _service;
        private readonly UserManager<User> _userManager;
        private readonly IProjectService projectService;

        public TaskController(ITaskService service, UserManager<User> userManager, IProjectService projectService)
        {
            _service = service;
            _userManager = userManager;
            this.projectService = projectService;
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
            var taskDto = await _service.GetTaskByIdAsync(id);
            if (taskDto == null) return NotFound();

            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = new SelectList(users, "Id", "UserName", taskDto.AssignedUserId);

            var createdByUser = await _userManager.FindByIdAsync(taskDto.CreatedByUserId.ToString());
            ViewBag.CreatedByUser = createdByUser?.UserName ?? "Unknown";

            return View(taskDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var users = await _userManager.Users.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "UserName", dto.AssignedUserId);

                var createdByUser = await _userManager.FindByIdAsync(dto.CreatedByUserId.ToString());
                ViewBag.CreatedByUser = createdByUser?.UserName ?? "Unknown";

                return View(dto);
            }

            var success = await _service.UpdateTaskAsync(dto);
            if (!success)
                return NotFound();

            Console.WriteLine($"Redirecting to project with ID: {dto.ProjectId}");
            return RedirectToAction("Details", "Project", new { id = dto.ProjectId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            var createdByUser = await _userManager.FindByIdAsync(task.CreatedByUserId.ToString());
            ViewBag.CreatedByUser = createdByUser?.UserName ?? "Unknown";
            var assignedByUser = await _userManager.FindByIdAsync(task.AssignedUserId?.ToString());
            ViewBag.AssignedByUser = assignedByUser?.UserName ?? createdByUser?.UserName;
            return task is null ? NotFound() : View(task);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, int ProjectId)
        {
            var success = await _service.DeleteTaskAsync(id);
            Console.WriteLine($"Redirecting to project with ID: {ProjectId}");
            return success ? RedirectToAction("Details", "Project", new { id = ProjectId }) : NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus)
        {
            var success = await _service.UpdateTaskStatusAsync(id, newStatus);
            return success ? Ok() : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateInColumn(TaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.GetUserAsync(User);
            dto.CreatedByUserId = user.Id;
            dto.AssignedUserId = user.Id;

            dto.Priority = "Normal";
            dto.DueDate = DateTime.Now.AddDays(7);

            await _service.CreateTaskAsync(dto);
            return RedirectToAction("Details", "Project", new { id = dto.ProjectId });
        }
    }
}
