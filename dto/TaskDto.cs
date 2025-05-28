using System.ComponentModel.DataAnnotations;

namespace TaskManager.dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заголовок обов'язковий")]
        [StringLength(100, ErrorMessage = "Заголовок не може бути довшим за 100 символів")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Опис обов'язковий")]
        public string? Description { get; set; }
        public string Status { get; set; } = "New";
        public string? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public int CreatedByUserId { get; set; }
        public List<int>? TagIds { get; set; } = new List<int>();
    }

}
