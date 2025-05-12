using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.dto;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllTagsAsync();
            return View(result);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TagDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.CreateTagAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }

}
