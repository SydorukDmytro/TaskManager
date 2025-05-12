namespace TaskManager.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }
        public User Creator { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
