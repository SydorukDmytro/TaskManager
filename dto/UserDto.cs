using System.ComponentModel.DataAnnotations;

namespace TaskManager.dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ім'я обов'язковий")]
        [StringLength(100, ErrorMessage = "Ім'я не може бути довшим за 100 символів")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Пошта обов'язкова")]
        [StringLength(100, ErrorMessage = "Пошта не може бути довшою за 100 символів")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
