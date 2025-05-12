using TaskManager.dto;

namespace TaskManager.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetCommentsByTaskAsync(int taskId);
        Task<CommentDto> CreateCommentAsync(CommentDto dto);
    }
}
