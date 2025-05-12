using TaskManager.dto;

namespace TaskManager.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllTagsAsync();
        Task<TagDto> CreateTagAsync(TagDto dto);
    }
}
