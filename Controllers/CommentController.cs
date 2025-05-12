using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.dto;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
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
    }

}
