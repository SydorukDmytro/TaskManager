using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.dto;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagerContext _context;

        public TaskService(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks
                .Include(t => t.TaskTags)
                .ToListAsync();
            return tasks.Select(t => t.ToDto());
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.TaskTags)
                .FirstOrDefaultAsync(t => t.TaskId == id);
            return task?.ToDto();
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto dto)
        {
            var task = dto.ToEntity();
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task.ToDto();
        }

        public async Task<bool> UpdateTaskAsync(TaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(dto.Id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.AssignedUserId = dto.AssignedUserId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByProjectAsync(int projectId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.TaskTags)
                .ToListAsync();
            return tasks.Select(t => t.ToDto());
        }
    }
}
