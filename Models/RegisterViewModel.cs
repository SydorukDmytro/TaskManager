using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
