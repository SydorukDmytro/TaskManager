using TaskManager.dto;

namespace TaskManager.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(int id);
        Task<ProjectDto> CreateProjectAsync(ProjectDto dto);
        Task<bool> UpdateProjectAsync(ProjectDto dto);
        Task<bool> DeleteProjectAsync(int id);
    }

}
