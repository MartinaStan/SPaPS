using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SPaPS.Models.AccountModels
{
    public class EditProfileInfoModel
    {
        public string Role { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Телефон")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Тип на корисник")]
        public int ClientTypeId { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Име и презиме")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Адреса")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Ембг")]
        public string IdNo { get; set; } = null!;

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Град")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Држава")]
        public int? CountryId { get; set; }

        //[Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Активности")]
        public List<long> Activities { get; set; }

        //[Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Број на вработени")]
        public int? NoOfEmployees { get; set; }

        //[Required(ErrorMessage = "Полето е задолжително")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Датум на основање")]
        public DateTime? DateOfEstablishment { get; set; }
    }
}
