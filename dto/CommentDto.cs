namespace TaskManager.dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }

        public int TaskId { get; set; }
    }
}
