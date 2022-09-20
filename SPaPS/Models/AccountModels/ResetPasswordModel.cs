using System.ComponentModel.DataAnnotations;

namespace SPaPS.Models.AccountModels
{
    public class ResetPasswordModel
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задолжително")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задолжително")]
        [Compare("NewPassword", ErrorMessage = "Лозинките не се совпаѓаат")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
