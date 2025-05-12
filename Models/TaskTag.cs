namespace TaskManager.Models
{
    public class TaskTag
    {
        public int TaskId { get; set; }
        public ProjectTask Task { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
