using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskManager.Services;


public class CommentsViewComponent : ViewComponent
{
    private readonly ICommentService _commentService;

    public CommentsViewComponent(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int taskId)
    {
        var comments = await _commentService.GetCommentsByTaskAsync(taskId);
        return View(comments);
    }
}
