using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManager.Data
{
    public class TaskManagerContext : IdentityDbContext<User, Role, int>
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User → Role
            builder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Project → CreatedByUser
            builder.Entity<Project>()
                .HasOne(p => p.Creator)
                .WithMany(u => u.CreatedProjects)
                .HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProjectTask → Project
            builder.Entity<ProjectTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId);

            // ProjectTask → AssignedUser
            builder.Entity<ProjectTask>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment → Task
            builder.Entity<Comment>()
                .HasOne(c => c.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskID);

            // Comment → Author
            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);

            // TaskTag many-to-many
            builder.Entity<TaskTag>()
                .HasKey(tt => new { tt.TaskId, tt.TagId });

            builder.Entity<TaskTag>()
                .HasOne(tt => tt.Task)
                .WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TaskId);

            builder.Entity<TaskTag>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TagId);

            // Первинні ключі (явно)
            builder.Entity<ProjectTask>().HasKey(t => t.TaskId);
            builder.Entity<Project>().HasKey(p => p.ProjectId);
            builder.Entity<Comment>().HasKey(c => c.CommentId);
            builder.Entity<Tag>().HasKey(t => t.TagId);
            builder.Entity<Role>().HasKey(r => r.Id);


        }

    }
}
