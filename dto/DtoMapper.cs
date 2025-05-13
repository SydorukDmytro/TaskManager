using Microsoft.AspNetCore.Identity;
using TaskManager.Models;

namespace TaskManager.dto
{
    public static class DtoMapper
    {
        // Task <-> TaskDto
        public static TaskDto ToDto(this Models.ProjectTask task)
        {
            return new TaskDto
            {
                Id = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                AssignedUserId = task.AssignedUserId,
                TagIds = task.TaskTags?.Select(tt => tt.TagId).ToList()
            };
        }

        public static Models.ProjectTask ToEntity(this TaskDto dto)
        {
            return new Models.ProjectTask
            {
                TaskId = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId,
                AssignedUserId = dto.AssignedUserId
                // TaskTags будуть заповнюватися окремо, якщо потрібно
            };
        }

        // Project <-> ProjectDto
        public static ProjectDto ToDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.ProjectId,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                CreatedByUserId = project.CreatedByUserId,
                CreatedByUserName = project.Creator?.FullName
            };
        }

        public static Project ToEntity(this ProjectDto dto)
        {
            return new Project
            {
                ProjectId = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt,
                CreatedByUserId = dto.CreatedByUserId
            };
        }

        // User <-> UserDto
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role?.Name
            };
        }

        public static User ToEntity(this UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                FullName = dto.FullName,
                Email = dto.Email,
                RoleId = dto.RoleId
            };
        }

        // Role <-> RoleDto
        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                RoleName = role.Name
            };
        }

        // Comment <-> CommentDto
        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.CommentId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                AuthorId = comment.AuthorID,
                AuthorName = comment.Author?.FullName,
                TaskId = comment.TaskID
            };
        }

        public static Comment ToEntity(this CommentDto dto)
        {
            return new Comment
            {
                CommentId = dto.Id,
                Content = dto.Content,
                CreatedAt = dto.CreatedAt,
                AuthorID = dto.AuthorId,
                TaskID = dto.TaskId
            };
        }

        // Tag <-> TagDto
        public static TagDto ToDto(this Tag tag)
        {
            return new TagDto
            {
                Id = tag.TagId,
                TagName = tag.TagName
            };
        }

        public static Tag ToEntity(this TagDto dto)
        {
            return new Tag
            {
                TagId = dto.Id,
                TagName = dto.TagName
            };
        }
    }
}
