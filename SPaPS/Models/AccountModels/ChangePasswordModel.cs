using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SPaPS.Models.AccountModels
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Стара лозинка")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Нова лозинка")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Потврди лозинка")]
        [Compare ("NewPassword", ErrorMessage ="Лозинките не се совпаѓаат")]
        
        public string ConfirmPassword { get; set; } = string.Empty;
    }
    
}
