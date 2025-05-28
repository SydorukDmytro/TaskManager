using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.dto;

namespace TaskManager.Models
{
    public class TaskEditViewModel
    {
        public TaskDto Task { get; set; }
        public User CreatedByUser { get; set; }
        public SelectList Users { get; set; } = default!;
    }
}
