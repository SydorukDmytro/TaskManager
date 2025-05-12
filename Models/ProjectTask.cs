using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class ProjectTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? DueDate { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? AssignedUserId { get; set; }
        public virtual User AssignedUser { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<TaskTag> TaskTags { get; set; }
    }
}
