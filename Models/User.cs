using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<ProjectTask> AssignedTasks { get; set; }
        public virtual ICollection<Project> CreatedProjects { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
