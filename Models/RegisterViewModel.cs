using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        [Display(Name = "Повне ім'я")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль є обов'язковим.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль повинен містити від 6 до 100 символів.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).+$",
            ErrorMessage = "Пароль має містити принаймні одну літеру та одну цифру.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }
    }
}
