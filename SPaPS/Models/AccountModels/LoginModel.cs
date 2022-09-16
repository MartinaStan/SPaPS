using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SPaPS.Models.AccountModels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Полето е задолжително")]
        [DisplayName("Корисничко име")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Лозинка")]
        public string Password { get; set; }
    }
}
