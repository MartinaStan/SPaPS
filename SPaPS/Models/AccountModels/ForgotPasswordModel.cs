using System.ComponentModel.DataAnnotations;

namespace SPaPS.Models.AccountModels
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Полето е задолжително")]
        [EmailAddress]  
        public string Email { get; set; }
    }
}
