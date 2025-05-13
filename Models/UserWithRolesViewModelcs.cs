using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Models
{
    public class UserWithRolesViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public List<SelectListItem> AvailableRoles { get; set; }
    }
}
