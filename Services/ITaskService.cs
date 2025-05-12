using TaskManager.dto;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksByProjectAsync(int projectId);
        Task<TaskDto?> GetTaskByIdAsync(int id);
        Task<TaskDto> CreateTaskAsync(TaskDto dto);
        Task<bool> UpdateTaskAsync(TaskDto dto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
