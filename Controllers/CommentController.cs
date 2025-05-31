using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.dto;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _service;
        private readonly UserManager<User> _userManager;
        private readonly ITaskService _taskService;

        public CommentController(ICommentService service, UserManager<User> userManager, ITaskService taskService)
        {
            _taskService = taskService;
            _userManager = userManager;
            _service = service;
        }

        public async Task<IActionResult> Index(int taskId)
        {
            var result = await _service.GetCommentsByTaskAsync(taskId);
            ViewBag.TaskId = taskId;
            return View(result);
        }

        public IActionResult Create(int taskId)
        {
            ViewBag.TaskId = taskId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.CreateCommentAsync(dto);
            return RedirectToAction("Index", new { taskId = dto.TaskId });
        }

        public async Task<IActionResult> CommentsPartial(int taskId)
        {
            var comments = await _service.GetCommentsByTaskAsync(taskId);
            return PartialView("_CommentsPartial", comments);
        }

        // AJAX: Додаємо коментар і повертаємо оновлений список
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Некоректні дані");

            dto.AuthorId = int.Parse(_userManager.GetUserId(User));

            await _service.CreateCommentAsync(dto);

            var updatedComments = await _service.GetCommentsByTaskAsync(dto.TaskId);
            return PartialView("_CommentsPartial", updatedComments);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _service.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();
            var task = await _taskService.GetTaskByIdAsync(comment.TaskId);

            await _service.DeleteCommentAsync(id);
            return RedirectToAction("Details", "Project", new {id = task.ProjectId});
        }
    }

}
