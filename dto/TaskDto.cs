namespace TaskManager.dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "New";
        public string? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedUserId { get; set; }

        public List<int>? TagIds { get; set; } = new List<int>();
    }

}
