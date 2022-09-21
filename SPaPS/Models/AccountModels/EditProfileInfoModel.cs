using System.ComponentModel;

namespace SPaPS.Models.AccountModels
{
    public class EditProfileInfoModel
    {


        /*ublic string UserId { get; set; } = null!;*/
        public string Email { get; set; } = null!;

        [DisplayName("Телефон")]
        public string? PhoneNumber { get; set; }

        [DisplayName("Тип на корисник")]
        public int ClientTypeId { get; set; }

        [DisplayName("Име и презиме")]
        public string Name { get; set; } = null!;

        [DisplayName("Адреса")]
        public string Address { get; set; } = null!;

        [DisplayName("Ембг")]
        public string IdNo { get; set; } = null!;

        [DisplayName("Град")]
        public int CityId { get; set; }

        [DisplayName(" ")]
        public int? CountryId { get; set; }
    }
}
