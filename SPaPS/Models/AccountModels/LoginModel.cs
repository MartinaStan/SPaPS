using System.ComponentModel;

namespace SPaPS.Models.AccountModels
{
    public class LoginModel
    {
        [DisplayName("Корисничко име")]
        public string Email { get; set; }
        [DisplayName("Лозинка")]
        public string Password { get; set; }
    }
}
