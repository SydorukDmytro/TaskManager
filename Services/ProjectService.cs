using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.dto;

namespace TaskManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly TaskManagerContext _context;

        public ProjectService(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _context.Projects.Include(p => p.Creator).ToListAsync();
            return projects.Select(p => p.ToDto());
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects.Include(p => p.Creator).FirstOrDefaultAsync(p => p.ProjectId == id);
            return project?.ToDto();
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectDto dto)
        {
            var entity = dto.ToEntity();
            entity.CreatedAt = DateTime.UtcNow;
            _context.Projects.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task<bool> UpdateProjectAsync(ProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(dto.Id);
            if (project == null) return false;

            project.Title = dto.Title;
            project.Description = dto.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
