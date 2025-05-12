namespace TaskManager.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AuthorID { get; set; }
        public User Author { get; set; }

        public int TaskID { get; set; }
        public ProjectTask Task { get; set; }
    }
}
