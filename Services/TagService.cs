using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.dto;

namespace TaskManager.Services
{
    public class TagService : ITagService
    {
        private readonly TaskManagerContext _context;

        public TagService(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            return tags.Select(t => t.ToDto());
        }

        public async Task<TagDto> CreateTagAsync(TagDto dto)
        {
            var tag = dto.ToEntity();
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag.ToDto();
        }
    }

}
