using System.ComponentModel.DataAnnotations;

namespace TaskManager.dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заголовок обов'язковий")]
        [StringLength(100, ErrorMessage = "Заголовок не може бути довшим за 100 символів")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Опис обов'язковий")]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByUserId { get; set; }
        public string? CreatedByUserName { get; set; }
        public ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
