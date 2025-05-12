using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.dto;

namespace TaskManager.Services
{
    public class CommentService : ICommentService
    {
        private readonly TaskManagerContext _context;

        public CommentService(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByTaskAsync(int taskId)
        {
            var comments = await _context.Comments
                .Where(c => c.TaskID == taskId)
                .Include(c => c.Author)
                .ToListAsync();

            return comments.Select(c => c.ToDto());
        }

        public async Task<CommentDto> CreateCommentAsync(CommentDto dto)
        {
            var comment = dto.ToEntity();
            comment.CreatedAt = DateTime.UtcNow;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment.ToDto();
        }
    }
}
