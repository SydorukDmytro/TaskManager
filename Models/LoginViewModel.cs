using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Логін")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль є обов'язковим.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль повинен містити від 6 до 100 символів.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).+$",
            ErrorMessage = "Пароль має містити принаймні одну літеру та одну цифру.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }
}
